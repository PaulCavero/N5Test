# N5Test
For the installation is necessary first:
- The first diagram explains the architecture implemented in the project. https://github.com/PaulCavero/N5Test/blob/master/Initial/Diagram/Arquitecture.drawio.png
- The second are the ends points created. https://github.com/PaulCavero/N5Test/blob/master/Initial/Diagram/DescriptionToEndPoints.png
-The last diagram is a design to database. https://github.com/PaulCavero/N5Test/blob/master/Initial/Diagram/DataBaseDiagram.PNG

- Run Script Database  https://github.com/PaulCavero/N5Test/blob/master/Initial/DataBase/ScriptDataBase.txt

- The second steps are run the scripts on Docker Compose:
* Elastic Search https://github.com/PaulCavero/N5Test/blob/master/Initial/Dockers/ElascticSearch/docker-compose.yaml

* Kafka https://github.com/PaulCavero/N5Test/blob/master/Initial/Dockers/Kafka/docker-compose.yaml

- Change the connection String to Local o Cloud Data Base
https://github.com/PaulCavero/N5Test/blob/master/N5Test/appsettings.Development.json

- The Last Step is create the default index on Elastic Search. Write "PUT /permission" on Dev Tools from Kibana in Local enviroment:
- For aditional informatio to end points https://github.com/PaulCavero/N5Test/blob/master/Initial/Diagram/DescriptionToEndPoints.png

