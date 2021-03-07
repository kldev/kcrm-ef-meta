CREATE VIEW aegis.app_activity as
select id,       
       'projects' as source,
       created,
       project_id as event_entity_id,
       event_name,
       (select event_data ->> 'entity_name') as entity_name,
       (select event_data ->> 'entity_ids')  as entity_ids,
       (select event_data ->> 'value')       as event_value,
       event_data
from project.project_activities