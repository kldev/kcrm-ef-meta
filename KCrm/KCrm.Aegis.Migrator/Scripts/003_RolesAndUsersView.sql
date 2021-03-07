CREATE VIEW aegis.user_and_roles as
SELECT
    UR.id,
    UR.role_id,
    UR.user_id,
    (
        select
            array_to_json(array_agg(row_to_json(u)))
        from
            (
                select
                    *
                from
                    app.user_roles
                where
                    app.user_roles.Id = UR.role_id
            ) u
    ) as Roles,
    (
        select
            array_to_json(array_agg(row_to_json(u)))
        from
            (
                select
                    id, username, email, created, provider
                from
                    app.user_accounts
                where
                        app.user_accounts.Id = UR.user_id
                order by
                    created desc
                    limit
          100
            ) u
    ) as TopUsers
FROM
    app.user_has_roles UR;  