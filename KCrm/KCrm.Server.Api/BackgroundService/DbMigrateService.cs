﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using KCrm.Aegis.Migrator;
using KCrm.Core.Definition;
using KCrm.Core.Entity.Common;
using KCrm.Core.Entity.Users;
using KCrm.Core.Security;
using KCrm.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace KCrm.Server.Api.BackgroundService {
    public class DbMigrateService : IHostedService {

        private readonly ILogger<DbMigrateService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _iServiceScopeFactory;
        private IPasswordHasher _hasher = new BCryptPasswordHasher ( );

        public DbMigrateService(ILogger<DbMigrateService> logger,
            IConfiguration configuration, IServiceScopeFactory iServiceScopeFactory) {
            _logger = logger;
            _configuration = configuration;
            _iServiceScopeFactory = iServiceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            try {
                var role = new UserRole ( ) { Id = Guid.NewGuid ( ), Description = "The root role", Name = UserRoleTypes.Root };
                var adminRole = new UserRole ( ) { Id = Guid.NewGuid ( ), Description = "The root role", Name = UserRoleTypes.Admin };
                var sellerRole = new UserRole ( ) { Id = Guid.NewGuid ( ), Description = "The root role", Name = UserRoleTypes.Seller };

                var randomRootPassword = Guid.NewGuid ( ).ToString ("N");
                var root = new User {
                    Username = "root",
                    Id = Guid.NewGuid ( ),
                    Email = "root@fakemail.fake",
                    Name = "Root",
                    LastName = string.Empty,
                    Password = _hasher.Hash (randomRootPassword),
                    IsEnabled = true,
                    UserRoles = new List<UserHasRole> ( ) { new UserHasRole (  ) {
                        Id = Guid.NewGuid (  ), UserRoleId = role.Id
                    }}
                };

                using (var scope = _iServiceScopeFactory.CreateScope ( )) {
                    var projectContext = scope.ServiceProvider.GetService<ProjectContext> ( );
                    var tagContext = scope.ServiceProvider.GetService<TagContext> ( );
                    var appUserContext = scope.ServiceProvider.GetService<AppUserContext> ( );
                    var commonContext = scope.ServiceProvider.GetService<CommonContext> ( );

                    await MigrateContext (cancellationToken, projectContext, tagContext, appUserContext, commonContext);

                    await SeedRolesIfNeeded (cancellationToken, appUserContext, role, adminRole, sellerRole);

                    await SeedRootIfNeeded (cancellationToken, appUserContext, randomRootPassword, root);

                    await SeedCountryIfNeed (cancellationToken, commonContext);

                }

                var migrator = new Migrator ( );
                migrator.UpdateDatabase (_configuration);
            }
            catch (Exception ex) {
                _logger.LogError ($"Migrate database failed: {ex.Message}");
            }
        }

        private async Task MigrateContext(CancellationToken cancellationToken, ProjectContext projectContext,
            TagContext tagContext, AppUserContext appUserContext, CommonContext commonContext) {
            _logger.LogInformation ($"Database - Migrate - Start");

            await projectContext.Database.MigrateAsync (cancellationToken);
            await tagContext.Database.MigrateAsync (cancellationToken);
            await appUserContext.Database.MigrateAsync (cancellationToken);
            await commonContext.Database.MigrateAsync (cancellationToken);

            _logger.LogInformation ($"Database - Migrate - End");
        }

        private async Task SeedRootIfNeeded(CancellationToken cancellationToken, AppUserContext appUserContext,
            string randomRootPassword, User root) {
            if (await appUserContext.AppUsers.CountAsync (cancellationToken) == 0) {
                _logger.LogInformation ($"Seed Roles - Start");

                _logger.LogInformation ($"Create root account with password: {randomRootPassword}");
                await appUserContext.AppUsers.AddAsync (root, cancellationToken);
                await appUserContext.SaveChangesAsync (cancellationToken);

                _logger.LogInformation ($"Seed Roles - End");
            }
        }

        private async Task SeedRolesIfNeeded(CancellationToken cancellationToken, AppUserContext appUserContext, UserRole role,
            UserRole adminRole, UserRole sellerRole) {
            if (await appUserContext.AppUserRoles.CountAsync (cancellationToken) == 0) {
                _logger.LogInformation ($"Seed Roles - Start");

                await appUserContext.AppUserRoles.AddRangeAsync (new[] { role, adminRole, sellerRole }, cancellationToken);
                await appUserContext.SaveChangesAsync (cancellationToken);

                _logger.LogInformation ($"Seed Roles - End");
            }
        }

        private async Task SeedCountryIfNeed(CancellationToken cancellationToken, CommonContext commonContext) {
            if (!(await commonContext.Countries.AnyAsync (cancellationToken))) {
                var json = @"[{""name"":""Afghanistan"",""iso"":""AF"",""code"":""004""},
{""name"":""Åland Islands"",""iso"":""AX"",""code"":""248""},{""name"":""Albania"",""iso"":""AL"",""code"":""008""},{""name"":""Algeria"",""iso"":""DZ"",""code"":""012""},{""name"":""American Samoa"",""iso"":""AS"",""code"":""016""},{""name"":""Andorra"",""iso"":""AD"",""code"":""020""},{""name"":""Angola"",""iso"":""AO"",""code"":""024""},{""name"":""Anguilla"",""iso"":""AI"",""code"":""660""},{""name"":""Antarctica"",""iso"":""AQ"",""code"":""010""},{""name"":""Antigua and Barbuda"",""iso"":""AG"",""code"":""028""},{""name"":""Argentina"",""iso"":""AR"",""code"":""032""},{""name"":""Armenia"",""iso"":""AM"",""code"":""051""},{""name"":""Aruba"",""iso"":""AW"",""code"":""533""},{""name"":""Australia"",""iso"":""AU"",""code"":""036""},{""name"":""Austria"",""iso"":""AT"",""code"":""040""},{""name"":""Azerbaijan"",""iso"":""AZ"",""code"":""031""},{""name"":""Bahamas"",""iso"":""BS"",""code"":""044""},{""name"":""Bahrain"",""iso"":""BH"",""code"":""048""},{""name"":""Bangladesh"",""iso"":""BD"",""code"":""050""},{""name"":""Barbados"",""iso"":""BB"",""code"":""052""},{""name"":""Belarus"",""iso"":""BY"",""code"":""112""},{""name"":""Belgium"",""iso"":""BE"",""code"":""056""},{""name"":""Belize"",""iso"":""BZ"",""code"":""084""},{""name"":""Benin"",""iso"":""BJ"",""code"":""204""},{""name"":""Bermuda"",""iso"":""BM"",""code"":""060""},{""name"":""Bhutan"",""iso"":""BT"",""code"":""064""},{""name"":""Bolivia (Plurinational State of)"",""iso"":""BO"",""code"":""068""},{""name"":""Bonaire, Sint Eustatius and Saba"",""iso"":""BQ"",""code"":""535""},{""name"":""Bosnia and Herzegovina"",""iso"":""BA"",""code"":""070""},{""name"":""Botswana"",""iso"":""BW"",""code"":""072""},{""name"":""Bouvet Island"",""iso"":""BV"",""code"":""074""},{""name"":""Brazil"",""iso"":""BR"",""code"":""076""},{""name"":""British Indian Ocean Territory"",""iso"":""IO"",""code"":""086""},{""name"":""Brunei Darussalam"",""iso"":""BN"",""code"":""096""},{""name"":""Bulgaria"",""iso"":""BG"",""code"":""100""},{""name"":""Burkina Faso"",""iso"":""BF"",""code"":""854""},{""name"":""Burundi"",""iso"":""BI"",""code"":""108""},{""name"":""Cabo Verde"",""iso"":""CV"",""code"":""132""},{""name"":""Cambodia"",""iso"":""KH"",""code"":""116""},{""name"":""Cameroon"",""iso"":""CM"",""code"":""120""},{""name"":""Canada"",""iso"":""CA"",""code"":""124""},{""name"":""Cayman Islands"",""iso"":""KY"",""code"":""136""},{""name"":""Central African Republic"",""iso"":""CF"",""code"":""140""},{""name"":""Chad"",""iso"":""TD"",""code"":""148""},{""name"":""Chile"",""iso"":""CL"",""code"":""152""},{""name"":""China"",""iso"":""CN"",""code"":""156""},{""name"":""Christmas Island"",""iso"":""CX"",""code"":""162""},{""name"":""Cocos (Keeling) Islands"",""iso"":""code"",""code"":""166""},{""name"":""Colombia"",""iso"":""CO"",""code"":""170""},{""name"":""Comoros"",""iso"":""KM"",""code"":""174""},{""name"":""Congo"",""iso"":""CG"",""code"":""178""},{""name"":""Congo, Democratic Republic of the"",""iso"":""CD"",""code"":""180""},{""name"":""Cook Islands"",""iso"":""CK"",""code"":""184""},{""name"":""Costa Rica"",""iso"":""CR"",""code"":""188""},{""name"":""Côte d''Ivoire"",""iso"":""CI"",""code"":""384""},{""name"":""Croatia"",""iso"":""HR"",""code"":""191""},{""name"":""Cuba"",""iso"":""CU"",""code"":""192""},{""name"":""Curaçao"",""iso"":""CW"",""code"":""531""},{""name"":""Cyprus"",""iso"":""CY"",""code"":""196""},{""name"":""Czechia"",""iso"":""CZ"",""code"":""203""},{""name"":""Denmark"",""iso"":""DK"",""code"":""208""},{""name"":""Djibouti"",""iso"":""DJ"",""code"":""262""},{""name"":""Dominica"",""iso"":""DM"",""code"":""212""},{""name"":""Dominican Republic"",""iso"":""DO"",""code"":""214""},{""name"":""Ecuador"",""iso"":""EC"",""code"":""218""},{""name"":""Egypt"",""iso"":""EG"",""code"":""818""},{""name"":""El Salvador"",""iso"":""SV"",""code"":""222""},{""name"":""Equatorial Guinea"",""iso"":""GQ"",""code"":""226""},{""name"":""Eritrea"",""iso"":""ER"",""code"":""232""},{""name"":""Estonia"",""iso"":""EE"",""code"":""233""},{""name"":""Eswatini"",""iso"":""SZ"",""code"":""748""},{""name"":""Ethiopia"",""iso"":""ET"",""code"":""231""},{""name"":""Falkland Islands (Malvinas)"",""iso"":""FK"",""code"":""238""},{""name"":""Faroe Islands"",""iso"":""FO"",""code"":""234""},{""name"":""Fiji"",""iso"":""FJ"",""code"":""242""},{""name"":""Finland"",""iso"":""FI"",""code"":""246""},{""name"":""France"",""iso"":""FR"",""code"":""250""},{""name"":""French Guiana"",""iso"":""GF"",""code"":""254""},{""name"":""French Polynesia"",""iso"":""PF"",""code"":""258""},{""name"":""French Southern Territories"",""iso"":""TF"",""code"":""260""},{""name"":""Gabon"",""iso"":""GA"",""code"":""266""},{""name"":""Gambia"",""iso"":""GM"",""code"":""270""},{""name"":""Georgia"",""iso"":""GE"",""code"":""268""},{""name"":""Germany"",""iso"":""DE"",""code"":""276""},{""name"":""Ghana"",""iso"":""GH"",""code"":""288""},{""name"":""Gibraltar"",""iso"":""GI"",""code"":""292""},{""name"":""Greece"",""iso"":""GR"",""code"":""300""},{""name"":""Greenland"",""iso"":""GL"",""code"":""304""},{""name"":""Grenada"",""iso"":""GD"",""code"":""308""},{""name"":""Guadeloupe"",""iso"":""GP"",""code"":""312""},{""name"":""Guam"",""iso"":""GU"",""code"":""316""},{""name"":""Guatemala"",""iso"":""GT"",""code"":""320""},{""name"":""Guernsey"",""iso"":""GG"",""code"":""831""},{""name"":""Guinea"",""iso"":""GN"",""code"":""324""},{""name"":""Guinea-Bissau"",""iso"":""GW"",""code"":""624""},{""name"":""Guyana"",""iso"":""GY"",""code"":""328""},{""name"":""Haiti"",""iso"":""HT"",""code"":""332""},{""name"":""Heard Island and McDonald Islands"",""iso"":""HM"",""code"":""334""},{""name"":""Holy See"",""iso"":""VA"",""code"":""336""},{""name"":""Honduras"",""iso"":""HN"",""code"":""340""},{""name"":""Hong Kong"",""iso"":""HK"",""code"":""344""},{""name"":""Hungary"",""iso"":""HU"",""code"":""348""},{""name"":""Iceland"",""iso"":""IS"",""code"":""352""},{""name"":""India"",""iso"":""IN"",""code"":""356""},{""name"":""Indonesia"",""iso"":""ID"",""code"":""360""},{""name"":""Iran (Islamic Republic of)"",""iso"":""IR"",""code"":""364""},{""name"":""Iraq"",""iso"":""IQ"",""code"":""368""},{""name"":""Ireland"",""iso"":""IE"",""code"":""372""},{""name"":""Isle of Man"",""iso"":""IM"",""code"":""833""},{""name"":""Israel"",""iso"":""IL"",""code"":""376""},{""name"":""Italy"",""iso"":""IT"",""code"":""380""},{""name"":""Jamaica"",""iso"":""JM"",""code"":""388""},{""name"":""Japan"",""iso"":""JP"",""code"":""392""},{""name"":""Jersey"",""iso"":""JE"",""code"":""832""},{""name"":""Jordan"",""iso"":""JO"",""code"":""400""},{""name"":""Kazakhstan"",""iso"":""KZ"",""code"":""398""},{""name"":""Kenya"",""iso"":""KE"",""code"":""404""},{""name"":""Kiribati"",""iso"":""KI"",""code"":""296""},{""name"":""Korea (Democratic People''s Republic of)"",""iso"":""KP"",""code"":""408""},{""name"":""Korea, Republic of"",""iso"":""KR"",""code"":""410""},{""name"":""Kuwait"",""iso"":""KW"",""code"":""414""},{""name"":""Kyrgyzstan"",""iso"":""KG"",""code"":""417""},{""name"":""Lao People''s Democratic Republic"",""iso"":""LA"",""code"":""418""},{""name"":""Latvia"",""iso"":""LV"",""code"":""428""},{""name"":""Lebanon"",""iso"":""LB"",""code"":""422""},{""name"":""Lesotho"",""iso"":""LS"",""code"":""426""},{""name"":""Liberia"",""iso"":""LR"",""code"":""430""},{""name"":""Libya"",""iso"":""LY"",""code"":""434""},{""name"":""Liechtenstein"",""iso"":""LI"",""code"":""438""},{""name"":""Lithuania"",""iso"":""LT"",""code"":""440""},{""name"":""Luxembourg"",""iso"":""LU"",""code"":""442""},{""name"":""Macao"",""iso"":""MO"",""code"":""446""},{""name"":""Madagascar"",""iso"":""MG"",""code"":""450""},{""name"":""Malawi"",""iso"":""MW"",""code"":""454""},{""name"":""Malaysia"",""iso"":""MY"",""code"":""458""},{""name"":""Maldives"",""iso"":""MV"",""code"":""462""},{""name"":""Mali"",""iso"":""ML"",""code"":""466""},{""name"":""Malta"",""iso"":""MT"",""code"":""470""},{""name"":""Marshall Islands"",""iso"":""MH"",""code"":""584""},{""name"":""Martinique"",""iso"":""MQ"",""code"":""474""},{""name"":""Mauritania"",""iso"":""MR"",""code"":""478""},{""name"":""Mauritius"",""iso"":""MU"",""code"":""480""},{""name"":""Mayotte"",""iso"":""YT"",""code"":""175""},{""name"":""Mexico"",""iso"":""MX"",""code"":""484""},{""name"":""Micronesia (Federated States of)"",""iso"":""FM"",""code"":""583""},{""name"":""Moldova, Republic of"",""iso"":""MD"",""code"":""498""},{""name"":""Monaco"",""iso"":""MC"",""code"":""492""},{""name"":""Mongolia"",""iso"":""MN"",""code"":""496""},{""name"":""Montenegro"",""iso"":""ME"",""code"":""499""},{""name"":""Montserrat"",""iso"":""MS"",""code"":""500""},{""name"":""Morocodeo"",""iso"":""MA"",""code"":""504""},{""name"":""Mozambique"",""iso"":""MZ"",""code"":""508""},{""name"":""Myanmar"",""iso"":""MM"",""code"":""104""},{""name"":""Namibia"",""iso"":""NA"",""code"":""516""},{""name"":""Nauru"",""iso"":""NR"",""code"":""520""},{""name"":""Nepal"",""iso"":""NP"",""code"":""524""},{""name"":""Netherlands"",""iso"":""NL"",""code"":""528""},{""name"":""New Caledonia"",""iso"":""NC"",""code"":""540""},{""name"":""New Zealand"",""iso"":""NZ"",""code"":""554""},{""name"":""Nicaragua"",""iso"":""NI"",""code"":""558""},{""name"":""Niger"",""iso"":""NE"",""code"":""562""},{""name"":""Nigeria"",""iso"":""NG"",""code"":""566""},{""name"":""Niue"",""iso"":""NU"",""code"":""570""},{""name"":""Norfolk Island"",""iso"":""NF"",""code"":""574""},{""name"":""North Macedonia"",""iso"":""MK"",""code"":""807""},{""name"":""Northern Mariana Islands"",""iso"":""MP"",""code"":""580""},{""name"":""Norway"",""iso"":""NO"",""code"":""578""},{""name"":""Oman"",""iso"":""OM"",""code"":""512""},{""name"":""Pakistan"",""iso"":""PK"",""code"":""586""},{""name"":""Palau"",""iso"":""PW"",""code"":""585""},{""name"":""Palestine, State of"",""iso"":""PS"",""code"":""275""}
,{""name"":""Panama"",""iso"":""PA"",""code"":""591""},{""name"":""Papua New Guinea"",""iso"":""PG"",""code"":""598""},{""name"":""Paraguay"",""iso"":""PY"",""code"":""600""},{""name"":""Peru"",""iso"":""PE"",""code"":""604""},{""name"":""Philippines"",""iso"":""PH"",""code"":""608""},{""name"":""Pitcairn"",""iso"":""PN"",""code"":""612""},{""name"":""Poland"",""iso"":""PL"",""code"":""616""},{""name"":""Portugal"",""iso"":""PT"",""code"":""620""},{""name"":""Puerto Rico"",""iso"":""PR"",""code"":""630""},{""name"":""Qatar"",""iso"":""QA"",""code"":""634""},{""name"":""Réunion"",""iso"":""RE"",""code"":""638""},{""name"":""Romania"",""iso"":""RO"",""code"":""642""},{""name"":""Russian Federation"",""iso"":""RU"",""code"":""643""},{""name"":""Rwanda"",""iso"":""RW"",""code"":""646""},{""name"":""Saint Barthélemy"",""iso"":""BL"",""code"":""652""},{""name"":""Saint Helena, Ascension and Tristan da Cunha"",""iso"":""SH"",""code"":""654""},{""name"":""Saint Kitts and Nevis"",""iso"":""KN"",""code"":""659""},{""name"":""Saint Lucia"",""iso"":""LC"",""code"":""662""},{""name"":""Saint Martin (French part)"",""iso"":""MF"",""code"":""663""},{""name"":""Saint Pierre and Miquelon"",""iso"":""PM"",""code"":""666""},{""name"":""Saint Vincent and the Grenadines"",""iso"":""VC"",""code"":""670""},{""name"":""Samoa"",""iso"":""WS"",""code"":""882""},{""name"":""San Marino"",""iso"":""SM"",""code"":""674""},{""name"":""Sao Tome and Principe"",""iso"":""ST"",""code"":""678""},{""name"":""Saudi Arabia"",""iso"":""SA"",""code"":""682""},{""name"":""Senegal"",""iso"":""SN"",""code"":""686""},{""name"":""Serbia"",""iso"":""RS"",""code"":""688""},{""name"":""Seychelles"",""iso"":""SC"",""code"":""690""},{""name"":""Sierra Leone"",""iso"":""SL"",""code"":""694""},{""name"":""Singapore"",""iso"":""SG"",""code"":""702""},{""name"":""Sint Maarten (Dutch part)"",""iso"":""SX"",""code"":""534""},{""name"":""Slovakia"",""iso"":""SK"",""code"":""703""},{""name"":""Slovenia"",""iso"":""SI"",""code"":""705""},{""name"":""Solomon Islands"",""iso"":""SB"",""code"":""090""},{""name"":""Somalia"",""iso"":""SO"",""code"":""706""},{""name"":""South Africa"",""iso"":""ZA"",""code"":""710""},{""name"":""South Georgia and the South Sandwich Islands"",""iso"":""GS"",""code"":""239""},{""name"":""South Sudan"",""iso"":""SS"",""code"":""728""},{""name"":""Spain"",""iso"":""ES"",""code"":""724""},{""name"":""Sri Lanka"",""iso"":""LK"",""code"":""144""},{""name"":""Sudan"",""iso"":""SD"",""code"":""729""},{""name"":""Suriname"",""iso"":""SR"",""code"":""740""},{""name"":""Svalbard and Jan Mayen"",""iso"":""SJ"",""code"":""744""},{""name"":""Sweden"",""iso"":""SE"",""code"":""752""},{""name"":""Switzerland"",""iso"":""CH"",""code"":""756""},{""name"":""Syrian Arab Republic"",""iso"":""SY"",""code"":""760""},{""name"":""Taiwan, Province of China"",""iso"":""TW"",""code"":""158""},{""name"":""Tajikistan"",""iso"":""TJ"",""code"":""762""},{""name"":""Tanzania, United Republic of"",""iso"":""TZ"",""code"":""834""},{""name"":""Thailand"",""iso"":""TH"",""code"":""764""},{""name"":""Timor-Leste"",""iso"":""TL"",""code"":""626""},{""name"":""Togo"",""iso"":""TG"",""code"":""768""},{""name"":""Tokelau"",""iso"":""TK"",""code"":""772""},{""name"":""Tonga"",""iso"":""TO"",""code"":""776""},{""name"":""Trinidad and Tobago"",""iso"":""TT"",""code"":""780""},{""name"":""Tunisia"",""iso"":""TN"",""code"":""788""},{""name"":""Turkey"",""iso"":""TR"",""code"":""792""},{""name"":""Turkmenistan"",""iso"":""TM"",""code"":""795""},{""name"":""Turks and Caicos Islands"",""iso"":""TC"",""code"":""796""},{""name"":""Tuvalu"",""iso"":""TV"",""code"":""798""},{""name"":""Uganda"",""iso"":""UG"",""code"":""800""},{""name"":""Ukraine"",""iso"":""UA"",""code"":""804""},{""name"":""United Arab Emirates"",""iso"":""AE"",""code"":""784""},{""name"":""United Kingdom of Great Britain and Northern Ireland"",""iso"":""GB"",""code"":""826""},{""name"":""United States of America"",""iso"":""US"",""code"":""840""},{""name"":""United States Minor Outlying Islands"",""iso"":""UM"",""code"":""581""},{""name"":""Uruguay"",""iso"":""UY"",""code"":""858""},{""name"":""Uzbekistan"",""iso"":""UZ"",""code"":""860""},{""name"":""Vanuatu"",""iso"":""VU"",""code"":""548""},{""name"":""Venezuela (Bolivarian Republic of)"",""iso"":""VE"",""code"":""862""},{""name"":""Viet Nam"",""iso"":""VN"",""code"":""704""},{""name"":""Virgin Islands (British)"",""iso"":""VG"",""code"":""092""},{""name"":""Virgin Islands (U.S.)"",""iso"":""VI"",""code"":""850""},{""name"":""Wallis and Futuna"",""iso"":""WF"",""code"":""876""},{""name"":""Western Sahara"",""iso"":""EH"",""code"":""732""},{""name"":""Yemen"",""iso"":""YE"",""code"":""887""},{""name"":""Zambia"",""iso"":""ZM"",""code"":""894""},{""name"":""Zimbabwe"",""iso"":""ZW"",""code"":""716""}]";

                var countries = JsonConvert.DeserializeObject<Country[]> (json);

                await commonContext.Countries.AddRangeAsync ((from x in countries
                                                              select new Country { Id = Guid.NewGuid ( ), Code = x.Code, Iso = x.Iso, Name = x.Name }), cancellationToken);

                await commonContext.SaveChangesAsync (cancellationToken);
            }

        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation (
                "DbMigrateService Hosted Service is stopping.");

            await Task.CompletedTask;
        }


    }
}
