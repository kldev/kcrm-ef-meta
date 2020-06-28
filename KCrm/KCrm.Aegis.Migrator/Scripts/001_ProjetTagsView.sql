-- drop view aegis.project_tags

 create view aegis.project_tags as
 select
     Projects.name as ProjectName,
     (select array_to_json(array_agg(row_to_json(t))) from
         (
             select
                 Tags.Name
             from
                 tag.tags as Tags
                     INNER JOIN project.project_has_tags PT on PT.tag_id = Tags.id
                     AND PT.project_id = Projects.id
         ) t )  as ProjectsTags
 from
     project.projects as Projects;