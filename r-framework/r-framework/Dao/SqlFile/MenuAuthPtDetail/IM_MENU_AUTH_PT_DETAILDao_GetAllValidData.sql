SELECT * FROM dbo.M_MENU_AUTH_PT_DETAIL
/*BEGIN*/WHERE
/*IF !data.PATTERN_ID.IsNull*/PATTERN_ID = /*data.PATTERN_ID.Value*//*END*/
/*IF data.FORM_ID != null*/AND FORM_ID = /*data.FORM_ID*//*END*/
/*IF !data.WINDOW_ID.IsNull*/AND WINDOW_ID = /*data.WINDOW_ID.Value*//*END*/
/*IF !data.AUTH_READ.IsNull*/ AND AUTH_READ = /*data.AUTH_READ*//*END*/
/*IF !data.AUTH_ADD.IsNull*/ AND AUTH_ADD = /*data.AUTH_ADD*//*END*/
/*IF !data.AUTH_EDIT.IsNull*/ AND AUTH_EDIT = /*data.AUTH_EDIT*//*END*/
/*IF !data.AUTH_DELETE.IsNull*/ AND AUTH_DELETE = /*data.AUTH_DELETE*//*END*/
/*IF data.BIKOU != null*/AND BIKOU = /*data.BIKOU*//*END*/
/*END*/