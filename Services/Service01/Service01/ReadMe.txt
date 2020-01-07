07 Jan 2020
-----------
# Useful dotnet commands
	dotnet restore
	dotnet build
	dotnet publish -c Release -o out

# Useful docker commands
	# Build using docker
	docker build -t service01 .

	# Cleanup
	docker ps -a | grep service01 | awk '{print $1}' | xargs docker stop
	docker ps -a | grep service01 | awk '{print $1}' | xargs docker rm
	docker images -a | grep service01 | awk '{print $3}' | xargs docker rmi
	docker images -a | grep none | awk '{print $3}' | xargs docker rmi

	# Run
	docker run --name service01 --env urls=http://0.0.0.0:6001/ -p 6001:6001 service01

# Also see Troubleshooting.txt
	As we can see, running even a simple controller was not straight forward
