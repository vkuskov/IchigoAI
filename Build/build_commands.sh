#!/bin/sh

build() {

    echo "Building..."

    xbuild /target:Clean /property:Configuration=Release IchigoAI.sln /verbosity:minimal
    xbuild /property:Configuration=Release IchigoAI.sln /verbosity:minimal

    echo "Build done!" 
}

runTests() {
    mono Deps/NSpec/NSpecRunner.exe IchigoAI.Test/bin/Release/IchigoAI.Test.dll
}

deploySingleProj() {
    srcName=$1
    destPath=$2

    echo "Deploy $srcName.dll -> $destPath/$srcName.dll"

    cp "$srcName/bin/Release/$srcName.dll" "$destPath/$srcName.dll"
}

deploy() {
    targetPath=$1
    
    echo "Deploy to $targetPath"

    echo "Deloy finished!"
}
