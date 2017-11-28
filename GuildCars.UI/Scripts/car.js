$(document).ready(function () {
    var path = window.location.pathname.toLowerCase();
    if ( path == "/inventory/detail" || path == "/sale/purchase") {
        GetDetail(window.location.search.split('=')[1]);
    } else if (path == "/inventory/new" || path == "/inventory/used" ||
                path == "/sale/list" || path == "/vehicle/list") {
        FillPrice();
        FillYear();
    } else if (path == "/home/home") {
        FillHome();
    }    
});

function GetCurUrl() {
    var path = window.location.pathname;
    var curUrl = window.location.href.replace(path, "");
    return curUrl;
}

function FillHome(type) {
    $('#divResult').empty();
    
    $.ajax({
        type: 'GET',
        url:  GetCurUrl() + '/showcar',
        success: function (cars) {
            $.each(cars, function (index, car) {
                var str = '';
                str += '    <div class="col-xs-3 divItemHome">';
                str += '        <img src="' + car.pictureUrl + '" alt="' + car.pictureName + '" class="imgShow" />';
                str += '    </div>';
                
                $('#divResult').append(str);
            })
        },
        error: function (xhr, textStatus, errorThrown) {

        }
    });
}


function FillPrice() {
    //$('#cboMinPrice').find('option').remove();
    //$('#cboMaxPrice').find('option').remove();

    $('#cboMinPrice').append($('<option>', { value: 0, text: 'No Min' }));
    $('#cboMaxPrice').append($('<option>', { value: 0, text: 'No Max' }));

    for (var i = 10000; i <= 100000; i += 10000) {
        $('#cboMinPrice').append($('<option>', { value: i, text: i }));
        $('#cboMaxPrice').append($('<option>', { value: i, text: i }));
    }
}

function FillYear() {
    $('#cboMinYear').append($('<option>', {value: 0, text: 'No Min'}));
    $('#cboMaxYear').append($('<option>', { value: 0, text: 'No Max' }));
    
    for (var i = 2010; i <= 2025; i ++) {
        $('#cboMinYear').append($('<option>', { value: i, text: i }));
        $('#cboMaxYear').append($('<option>', { value: i, text: i }));
    }
}

function GetInventory(type) {
    $('#divResult').empty();

    var filter = "ALL";
    if ($("#txtSearch").val() != "") {
        filter = $("#txtSearch").val();
    }

    var minPrice = 0;
    if ($("#cboMinPrice").val() != 0) {
        minPrice = $("#cboMinPrice").val();
    }

    var maxPrice = 0;
    if ($("#cboMaxPrice").val() != 0) {
        maxPrice = $("#cboMaxPrice").val();
    }

    var minYear = 0;
    if ($("#cboMinYear").val() != 0) {
        minYear = $("#cboMinYear").val()
    }
    var maxYear = 0;
    if ($("#cboMaxYear").val() != 0) {
        maxYear = $("#cboMaxYear").val();
    }
    $.ajax({
        type: 'GET',
        url: GetCurUrl() + '/car/' + filter + '/' + minPrice + '/' + maxPrice + '/' + minYear + '/' + maxYear + '/' + type,
        success: function (cars) {
            $.each(cars, function (index, car) {
                var str = '';
                str += '<div class="row divSearch">';
                str += '<div class="row" style = "text-align:center">';
                str += '    <div class="col-xs-3">';
                str += '        <label>' + car.releaseYear + ' ' + car.make.description + ' ' + car.model.description + '</label><br />';
                str += '    </div>';
                str += '    </div>';
                str += '<div class="row" style="font-weight:bold">';
                str += '    <div class="col-xs-3" style = "height:200px">';
                str += '        <img src="' + car.pictureUrl + '" alt="' + car.pictureName + '" class="imgShow" />';
                str += '    </div>';
                str += '    <div class="col-xs-3">';
                str += '        <div class="divItem">';
                str += '            Body Style : ' + car.bodyStyle + '<br />';
                str += '        </div>';
                str += '        <div class="divItem">';
                str += '            Tran : ' + car.transmission + '<br />';
                str += '        </div>';
                str += '        <div class="divItem">';
                str += '            <b>Color : ' + car.color;
                str += '        </div>';
                str += '    </div>';
                str += '    <div class="col-xs-3">';
                str += '        <div class="divItem">';
                str += '            Interior : ' + car.interior + '<br />';
                str += '        </div>';
                str += '        <div class="divItem">';
                str += '            Mileage : ' + car.mileage + '<br />';
                str += '        </div>';
                str += '        <div class="divItem">';
                str += '            VIN # : ' + car.vinNo;
                str += '        </div>';
                str += '    </div>';
                str += '    <div class="col-xs-3">';
                str += '        <div class="divItem">';
                str += '            Sale Price : ' + car.salePrice + '<br />';
                str += '        </div>';
                str += '        <div class="divItem">';
                str += '            MSRP : ' + car.msrp + '<br />';
                str += '        </div>';
                str += '        <div style = "text-align:right">';
                if (window.location.pathname.toLowerCase() == "/sale/list") {
                    str += '        <input type = "button" class="btn btn-detail" onclick="' + 'window.location = ' + "'" + GetCurUrl() + '/Sale/Purchase?id=' + car.carId + "'" + '" value = "Purchase" />';                    
                } else if (window.location.pathname.toLowerCase() == "/vehicle/list"){
                    str += '        <input type = "button" class="btn btn-detail" onclick="' + 'window.location = ' + "'" + GetCurUrl() + '/Vehicle/Edit?id=' + car.carId + "'" + '" value = "Edit" />';                    
                } else {
                    str += '        <input type = "button" class="btn btn-detail" onclick="' + 'window.location = ' + "'" + GetCurUrl() + '/Inventory/Detail?id=' + car.carId + "'" + '" value = "Detail" />';                    
                }
                str += '        </div>';
                str += '    </div>';
                str += '</div>';

                $('#divResult').append(str);
            })
        },
        error: function (xhr, textStatus, errorThrown) {

        }
    });
}

function GetDetail(id) {
    $.ajax({
        type: 'GET',
        url: GetCurUrl() + '/car/' + id,
        success: function (car) {
            var str = '';
            str += '<div class="row divSearch">';
            str += '<div class="row" style = "text-align:center">';
            str += '    <div class="col-xs-3">';
            str += '        <label>' + car.releaseYear + ' ' + car.make.description + ' ' + car.model.description + '</label><br />';
            str += '    </div>';
            str += '</div>';
            str += '<div class="row" style="font-weight:bold">';
            str += '    <div class="col-xs-3">';
            str += '        <img src="' + car.pictureUrl + '" alt="' + car.pictureName + '" class="imgShow" />';
            str += '    </div>';
            str += '    <div class="col-xs-9">';
            str += '        <div class = "row">';
            str += '            <div class="col-xs-4">';
            str += '                <div class="divItem">';
            str += '                    Body Style : ' + car.bodyStyle + '<br />';
            str += '                </div>';
            str += '                <div class="divItem">';
            str += '                    Tran : ' + car.transmission + '<br />';
            str += '                </div>';
            str += '                <div class="divItem">';
            str += '                    <b>Color : ' + car.color;
            str += '                </div>';
            str += '            </div>';
            str += '            <div class="col-xs-4">';
            str += '                <div class="divItem">';
            str += '                    Interior : ' + car.interior + '<br />';
            str += '                </div>';
            str += '                <div class="divItem">';
            str += '                    Mileage : ' + car.mileage + '<br />';
            str += '                </div>';
            str += '                <div class="divItem">';
            str += '                    VIN # : ' + car.vinNo;
            str += '                </div>';
            str += '            </div>';
            str += '            <div class="col-xs-4">';
            str += '                <div class="divItem">';
            str += '                    Sale Price : ' + car.salePrice + '<br />';
            str += '                </div>';
            str += '                <div class="divItem">';
            str += '                    MSRP : ' + car.msrp + '<br />';
            str += '                </div>';
            str += '            </div>';
            str += '        </div>';
            str += '        <div class = "row">';
            str += '            <div class="col-xs-12">';
            str += '            ' + car.description + '<br />';
            str += '            </div>';
            str += '        </div>';
            str += '    </div>'
            str += '</div>'
            str += '<div class="row" style="text-align:right">';
            str += '    <div class="col-xs-12">';
            if (window.location.pathname == "/Sale/Purchase") {
                $("#CarId").val(car.carId);
            } else {
                if (car.isPurchase) {
                    str += '        <b>Unavailable - SOLD</b>';
                } else {
                    str += '        <input type = "button" class="btn btn-detail" onclick="' + 'window.location = ' + "'" + GetCurUrl() + '/Contact/Add?id=' + car.carId + "'" + '" value = "Contact Us" />';                    
                }
            }
            
            str += '    </div>';
            str += '</div>';

            $('#divDetail').append(str);

        },
        error: function (xhr, textStatus, errorThrown) {

        }
    });
}


function Purchase() {
    var haveValidationErrors = checkAndDisplayValidationErrors($('#divAdd').find('input'), "#divError");
    if (haveValidationErrors) {
        return false;
    }

    var carId = $("#CarId").val();
    var customerName = $("#CustomerName").val();
    var phone = $("#Phone").val();
    var email = $("#Email").val();
    var street1 = $("#Street1").val();
    var street2 = $("#Street2").val();
    var city = $("#City").val();
    var zipCode = $("#ZipCode").val();
    var state = $("#State").val();
    var purchasePrice = $("#PurchasePrice").val();
    var purchaseType = $("#PurchaseType").val()

    $.ajax({
        type: 'POST',
        url: GetCurUrl() + '/sale',
        data: JSON.stringify({
            carId: carId,
            customerName: customerName,
            phone: phone,
            email: email,
            street1: street1,
            street2: street2,
            city: city,
            zipCode: zipCode,
            state: state,
            purchasePrice: purchasePrice,
            purchaseType: purchaseType
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',

        success: function () {
            window.location = GetCurUrl() + '/Home/Home';
        },
        error: function (xhr, textStatus, errorThrown) {

        }
    });
}

function checkAndDisplayValidationErrors(input, errorLocation) {
    $(errorLocation).empty();
    var errorMessages = [];

    input.each(function () {
        // Use the HTML5 validation API to find the validation errors
        if (!this.validity.valid) {
            var errorField = $('label[for=' + this.id + ']').text();
            errorMessages.push(errorField + ', ' + this.validationMessage.toString().toLowerCase());
        }
    });

    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $(errorLocation).show();
            $(errorLocation).append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        return true;
    } else {
        return false;
    }
}