$(document).ready(function () {
    FillModel($("#MakeId").val());
});

$("#MakeId").change(function () {
    FillModel($("#MakeId").val());
});


function GetCurUrl() {
    var path = window.location.pathname;
    var curUrl = window.location.href.replace(path, "");
    return curUrl;
}

function FillModel(makeId) {
    $("#ModelId").find('option').remove();
    $.ajax({
        type: 'GET',
        url: GetCurUrl() + '/makes/' + makeId,
        success: function (cars) {
            $.each(cars, function (index, car) {
                $('#ModelId').append($('<option>', { value: car.modelId, text: car.description }));
            });
        },
        error: function (xhr, textStatus, errorThrown) {

        }
    });

}