
create view aegis.project_started_stats as
SELECT
    cast(date_part('month', start_date_time_utc) as int) as monthNumber,
    to_char(start_date_time_utc, 'month') as month,
    to_char(start_date_time_utc, 'YYYY') as year,
    count(date_part('month', start_date_time_utc)) as count

FROM
    project.projects
WHERE
    start_date_time_utc is not null
GROUP by
    date_part('month', start_date_time_utc),
    to_char(start_date_time_utc, 'month'),
    to_char(start_date_time_utc, 'YYYY');