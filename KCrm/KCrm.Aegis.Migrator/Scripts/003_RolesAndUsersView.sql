CREATE VIEW aegis.user_and_roles as
SELECT
    UR.id,
    UR.user_role_id,
    UR.user_id,
    (
        select
            array_to_json(array_agg(row_to_json(u)))
        from
            (
                select
                    *
                from
                    app.app_user_roles
                where
                    app.app_user_roles.Id = UR.user_role_id
            ) u
    ) as Roles,
    (
        select
            array_to_json(array_agg(row_to_json(u)))
        from
            (
                select
                    *
                from
                    app.app_users
                where
                        app.app_users.Id = UR.user_id
                order by
                    created desc
                    limit
          100
            ) u
    ) as TopUsers
FROM
    app.user_has_roles UR;    