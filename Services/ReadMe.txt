08 Jan 2020
-----------

(#) Add docker-compose to launch the only service at this time. 
	> docker compose up &
	> docker compose down

And I used a crude way to spawn 2 instances of service01 :)
	$ docker-compose up
	$ curl http://localhost:6001/api/Service01
	$ curl http://localhost:6002/api/Service01
