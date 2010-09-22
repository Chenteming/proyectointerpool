$number = 1;
while ( <> ){
	if(/\d+.-.(.*),.(.*)$/){
		print "insert into [interpooldb].[dbo].[Cities] (CityName, CityCountry, CityNumber, Level_LevelId) values (\'" . $1 . "\',\'" .  $2 .  "\', $number, (Select max(LevelId) FROM [interpooldb].[dbo].[Levels]));" . "\n";
		$number = $number + 1;
	}
}