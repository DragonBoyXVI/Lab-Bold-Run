extends Node


## The key checked for what locale the translation belongs to.
const LOCALE_CHECK := "locale"


## Contains extra translation data.
## eg. An array of text you can randomly pick from.
var _extra_data: Dictionary[ String, Dictionary ] = {}


## This node stores any extra data that cant be stored in a translation.
## For example, your json can contain an array of strings, and this will
## store that here for you to grab later.
## Can return null if the extra data doesnt exist.
func get_extra_data( key: String, locale: String = TranslationServer.get_locale() ) -> Variant:
	
	if ( not _extra_data.has( locale ) ):
		push_error( "Getting extra data for a locale we dont have! ", locale )
		return null
	
	var data: Dictionary = _extra_data[ locale ]
	if ( not key in data ):
		push_error( "Getting extra data that we dont have! ", locale, " ", key )
		return null
	
	return data[ key ]


## Loads a file and parses it as text, can be a file inside the "res" or outside.[br]
## if the file is a json that contains a valid dictionary, it gets parsed
## as a translation. Otherwise an error is printed and nothing happens.
## The dictionary is parsed via "parse_dict_to_translation"
func parse_file_for_dict( path: String ) -> void:
	
	if ( not FileAccess.file_exists( path ) ):
		push_error( "Trying to translate a file that doesnt exist! ", path )
		return
	
	var file := FileAccess.open( path, FileAccess.READ )
	if ( not file ):
		push_error( "Translation file open error! Path: ", path, " Error: ", error_string( FileAccess.get_open_error() ) )
		return
	
	var file_string: String = file.get_as_text()
	var json_parsed: Variant = JSON.parse_string( file_string )
	if ( json_parsed == null ):
		push_error( "Translation file json parse failed! ", path )
		return
	
	if ( typeof( json_parsed ) == TYPE_DICTIONARY ):
		
		parse_dict_to_translation( json_parsed )
	else:
		
		push_error( "Translation file parsed, but the parsed type was not a Dictionary! ", path )
		return

## Parses the provided dictionary into a translation,
## and adds it to the [TranslationServer].[br]
## NOTE: If this defines extra data that is already defined for that language,
## it will be overwritten by the most recent parse.[br]
## If the dict is invalid, nothing happens and an error is printed.
func parse_dict_to_translation( translation_dict: Dictionary ) -> Translation:
	
	if ( not translation_dict.has( LOCALE_CHECK ) ):
		push_error( "Translation dict does not contain a locale key! Translations must have a \"", LOCALE_CHECK, "\" key." )
		return
	
	var translation := Translation.new()
	var extra: Dictionary = {}
	for key: Variant in translation_dict.keys():
		
		# we can only add strings as keys
		if ( typeof( key ) != TYPE_STRING ):
			continue
		
		var value: Variant = translation_dict[ key ]
		if ( typeof( value ) == TYPE_STRING ):
			
			translation.add_message( key, value )
		else:
			
			extra[ key ] = value
	
	var locale: String = translation_dict[ LOCALE_CHECK ]
	translation.locale = locale
	TranslationServer.add_translation( translation )
	_extra_data[ locale ] = extra
	return translation
