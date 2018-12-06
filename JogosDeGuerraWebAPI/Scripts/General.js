const ops = 'Oops...';

function showError(jqXHR) {

    var response = jqXHR.responseJSON,
        error_message = null;

    if (response) {

        if (response.ModelState) {
            error_message = response.ModelState[""][0];
        } else {
            error_message = response.error_description;
        }

        errorAlert(ops, error_message);
    }
}

function errorAlert(title, message) {
    $.alert({
        title: title,
        content: message,
        theme: 'modern',
    });

    return false;
}