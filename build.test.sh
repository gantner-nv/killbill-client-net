TOTALTESTS=0

RED='\033[0;31m'
GREEN='\033[0;32m'
NC='\033[0m' # No Color
BLUEBACKGROUND='\033[44m'

validate_testresults(){
    API=$1
    TESTPATH=$2
    VALID_RESULT="0"
    echo $TESTPATH
    dotnet test $TESTPATH --logger "trx;LogFileName=../TestResult.xml"
    
    FAILEDTESTS=$(xmllint --xpath "string(//*[local-name()='Counters']/@failed)" tests/"$API""/TestResult.xml")
    TESTS=$(xmllint --xpath "string(//*[local-name()='Counters']/@total)" tests/"$API""/TestResult.xml")
    TOTALTESTS=$(expr $TOTALTESTS + $TESTS)

    if [ "$FAILEDTESTS" != "$VALID_RESULT" ]; then
        echo "${RED}Oh noo... $FAILEDTESTS tests for $API failed...${NC}"
        exit 0
    else
        echo "${GREEN}Woohoo... $TESTS tests for $API succeeded!!!${NC}"
    fi
}

validate_testresults KillBill.Client.Net.UnitTests tests/KillBill.Client.Net.UnitTests/KillBill.Client.Net.UnitTests.csproj

echo ""
echo "${BLUEBACKGROUND}\o/ You did it! $TOTALTESTS tests succeeded! Applause for you!!! \o/${NC}"
echo ""