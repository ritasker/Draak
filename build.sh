#! /usr/bin/env bash

echo 'Building .Net Solution'
dotnet build ./src/Pier8.Draak.sln

echo 'Building UI Project'
pushd ./src/draak-ui
npm install
npm run build
echo 'Running UI Tests'
npm run test:unit
popd