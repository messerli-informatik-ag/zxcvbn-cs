# Changelog

## 1.0
- Initial release
- Added keyboard layouts: 
	Swiss German, Swiss French, Italian, German, French
- Changed date matcher logic:
	Considers the different date format by country
- Changed dictionaries load
	Loads all embedded resources from the directory "zxcvbn.Dictionaries"

## 2.0
- Renamed class "Zxcvbn" to "PasswordMetric"
- Renamed class "Result" to "PasswordMetricResult"
- Renamed start index in the password string of the matched token "i" to "Begin"
- Renamed end index in the password string of the matched token "j" to "End"
- Renamed translation value "France" to "French"
- Added translation value "Italian"