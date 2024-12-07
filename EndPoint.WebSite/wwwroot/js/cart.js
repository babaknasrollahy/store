function AddCount(CartitemId) {
    debugger;
    let postData = {
        'itemId': CartitemId,
    }
    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "Cart/Increase",
        data: postData,
        success: function (data) {
            location.reload();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function lowCount(CartitemId) {
    let postData = {
        'itemId': CartitemId,
    }
    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "Cart/Decrease",
        data: postData,
        success: function (data) {
            location.reload();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}

function Delete(CartitemId) {
    let postData = {
        'itemId': CartitemId,
    }
    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "Cart/Delete",
        data: postData,
        success: function (data) {
            location.reload();
        },
        error: function (request, status, error) {
            alert(request.responseText);
        }
    });
}