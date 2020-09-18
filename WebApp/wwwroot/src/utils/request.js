


const request =
{
    Get(url, object, callback, options) {

        var ajaxObject = {
            // The URL for the request
            url: url,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            traditional:true,
            success: function (response) {

            },
            error: function (response) {

            }
        };
        
        if (typeof object !== "undefined" && object!==null) {
            ajaxObject["data"] = object;
        }
        if (typeof options !== "undefined") {
            $.extend(ajaxObject, options);
        }
        $.ajax(ajaxObject)
            // Code to run if the request succeeds (is done);
            // The response is passed to the function
            .done(function (response) {

                if (typeof callback !== "undefined" && typeof callback === "function") {
                    callback(response);
                }
            })
            // Code to run if the request fails; the raw request and
            // status codes are passed to the function
            .fail(function (xhr, status, errorThrown) {
            })
            // Code to run regardless of success or failure;
            .always(function (xhr, status) {
            });


    },
    Post(url, object, callback, options) {
        var ajaxObject = {
            // The URL for the request
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "JSON",
            //traditional: true,
            success: function (response) {
            },
            error: function (response) {
                console.log(response);
                if (response.isError) {
                    var errors = response.responseException.exceptionMessage.errors;
                    console.log(errors);
                }
              
            }
        };
        if (typeof options !== "undefined") {
            $.extend(ajaxObject, options);
        }

        if (typeof object !== "undefined" && object !== null) {
            ajaxObject["data"] = JSON.stringify(object);
        }
        $.ajax(ajaxObject)
            // Code to run if the request succeeds (is done);
            // The response is passed to the function
            .done(function (response) {
                if (typeof callback !== "undefined" && typeof callback === "function") {
                    callback(response);
                }
            })
            // Code to run if the request fails; the raw request and
            // status codes are passed to the function
            .fail(function (xhr, status, errorThrown) {
            })
            // Code to run regardless of success or failure;
            .always(function (xhr, status) {
            });
    },
    Upload(url, object, callback) {


        $.ajax({
            // The URL for the request
            url: url,
            type: "POST",
            data: object,
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess) {
                    showToast("Success!", response.Message, "success");
                } else {
                    showToast("Error!", response.Message, "error");
                }
                callback();
            },
            error: function () {
                showToast("Error!", "Fatal Error!", "error");
            }
        })
            // Code to run if the request succeeds (is done);
            // The response is passed to the function
            .done(function (json) {


            })
            // Code to run if the request fails; the raw request and
            // status codes are passed to the function
            .fail(function (xhr, status, errorThrown) {
                showToast("Error!", status, "error");
            })
            // Code to run regardless of success or failure;
            .always(function (xhr, status) {
            });


    }
}

