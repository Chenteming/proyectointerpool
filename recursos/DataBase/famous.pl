while ( <> ){
	if(/(.*)\,(.*)\,(.*)/){
		print "insert into [interpooldb].[dbo].[HardCodedSuspects] (HardCodedSuspectFirstName, HardCodedSuspectLastName, HardCodedSuspectGender) values (\'" . $1 . "\', \'" . $2 . "\',\'" . $3 . "\');" . "\n";
	}
}