/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    // Define changes to default configuration here. For example:
    config.filebrowserBrowseUrl = location.hash + '/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = location.hash + '/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = location.hash + '/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = location.hash + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = location.hash + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = location.hash + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    
    config.toolbar =[ ['Source','-','-','Templates'],
     ['Cut','Copy','Paste','PasteText','-','Print', 'Scayt'],
     ['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat'],
     '/',
     ['Bold','Italic','Underline','Strike','-','Subscript','Superscript'],
     ['NumberedList','BulletedList','-','Outdent','Indent'],
     ['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
     ['Link','Unlink','Anchor'],
     ['Image','Table','HorizontalRule','Smiley','SpecialChar'],
     '/',
     ['Styles','Format','Font','FontSize'],
     ['TextColor','BGColor'],
     ['Maximize', 'ShowBlocks']
	 ];
};
