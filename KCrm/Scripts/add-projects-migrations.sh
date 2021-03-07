#!/usr/bin/bash

if [ -z "$1" ]; then
	echo "Migration name is requried"
	exit -1
fi

NAME="$1"

echo "Migration name $NAME"
cd ../KCrm
echo $(cd . && pwd)

dotnet ef  migrations add $NAME -o __Migrations/Projects -c KCrm.Data.Projects.ProjectContext -p KCrm.Data --startup-project KCrm.Server.Api

#dotnet ef  migrations remove -f -c KCrm.Data.Context.ProjectContext -p KCrm.Data --startup-project KCrm.Server.Api

