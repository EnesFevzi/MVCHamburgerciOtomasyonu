$(document).ready(function () {
    $("#btnSave").click(function (event) {
        event.preventDefault();

        var addUrl = app.Urls.menuSizeAddUrl;
        var redirectUrl = app.Urls.menuAddUrl;

        var menuSizeAddDto = {
            SizeName: $("input[id=sizeName]").val(),
            PriceModifier: $("input[id=priceModifier]").val() 
        }

        var jsonData = JSON.stringify(menuSizeAddDto);
        console.log(jsonData);

        $.ajax({
            url: addUrl,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                setTimeout(function () {
                    window.location.href = redirectUrl;
                }, 1500);
            },
            error: function () {
                toast.error("Bir hata oluştu.", "Hata");
            }
        });
    });
});
