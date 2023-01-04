function OverrideStatusVal()
{ 
    var valInitials = document.getElementById('PageData_Initials').value;
    if (valInitials.length < 1) {
        alert('Please add your initials.');
        return false;
    }

    var valComment = document.getElementById('PageData_Comment').value;
    if (valComment.length < 1) {
        alert('Please add a comment regarding why you are changing the status.');
        return false;
    }

    // this is needed as there is we cannot limit the ui from typing more than 175 as it is a multiline txt box
    //if (valComment.length > 175) {
    //    alert('Comment must be less than 175 letters.');
    //    return false; 
    //}

    document.forms[0].submit();
}
