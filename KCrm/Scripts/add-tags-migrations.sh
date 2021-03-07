#!/usr/bin/bash

if [ -z "$1" ]; then
	echo "Migration name is requried"
	exit -1
fi

NAME="$1"

echo "Migration name $NAME"
cd ../KCrm
echo $(cd . && pwd)

dotnet ef  migrations add $NAME -o __Migrations/Tags -c KCrm.Data.Tags.TagContext -p KCrm.Data --startup-project KCrm.Server.Api
