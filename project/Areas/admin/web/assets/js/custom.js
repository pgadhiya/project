
$("#state_id").change(function () {
    FillStateDropdown();
});

$(document).ready(function () {
    FillStateDropdown();
});

function FillStateDropdown() {
    $.ajax({
        url: "/admin/Admin/GetCitiesByStateId",
        type: "GET",
        data: { id: $("#state_id").val() },
        dataType: "json",
        success: function (cities) {
            $("#city_id").empty();
            $("#city_id").append("<option>--- Select City ---</option>");
            $.each(cities, function (key, item) {
                $("#city_id").append("<option value='" + item.CT_ID + "'>" + item.Cityname + "</option>");
            });
        }
    });
}
