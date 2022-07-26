docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic dev.system.auditlog2; 
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic dev.system.permission
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic dev.healthCheck
docker exec -it kafka kafka-topics --bootstrap-server localhost:9092 --create --topic dev.system.blocked-players-log