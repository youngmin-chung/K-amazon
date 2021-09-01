function cur(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    let sign = num === (num = Math.abs(num));
    num = Math.floor(num * 100 + 0.50000000001);
    let cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
        num = num.substring(0, num.length - (4 * i + 3)) + ',' +
            num.substring(num.length - (4 * i + 3));
    return sign ? '' : /*'-' + */'$' + num + '.' + cents;
} //cur

$(function () {
    if ($("#register_popup") !== undefined) {
        $('#register_popup').modal('show');
    }

    if ($("#login_popup") !== undefined) {
        $('#login_popup').modal('show');
    }


    $(".nav-tabs a").on("show.bs.tab", function (e) {
        if ($(e.relatedTarget).text() === "Personal") { // tab 1
            $("#Firstname").valid();
            $("#Lastname").valid();
            $("#Age").valid();
            $("#CreditcardType").valid();
            if ($("#Firstname").valid() === false || $("#Lastname").valid() === false) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === "Address") { // tab 2
            $("#Address1").valid();
            $("#City").valid();
            $("#Country").valid();
            $("#Region").valid();
            $("#Mailcode").valid();
            if ($("#Address1").valid() === false || $("#City").valid() === false) {
                return false; // suppress click
            }
        }
        if ($(e.relatedTarget).text() === "Account") { // tab 3
            $("#Email").valid();
            $("#Password").valid();
            $("#RepeatPassword").valid();
            if ($("#Email").valid() === false || $("#Password").valid() === false ||
                $("#RepeatPassword").valid() === false) {
                return false; // suppress click
            }
        }
    }); // show bootstrap tab

    // before catalouge - details click - to load popup on catalogue
    //$("a.btn-outline-dark").on("click", (e) => {
    //    $("#results").text("");
    //    let id = e.target.dataset.id;
    //    let data = JSON.parse(e.target.dataset.details); // it's a string need an object
    //    CopyToModal(id, data);
    //});

    // re-pop modal to show newly created add message
    if ($('#selectedId').val() !== '' && $('#selectedId').val() !== undefined) {
        let data = $("#branbtn" + $("#selectedId").val()).data("details");
        CopyToModal($("#selectedId").val(), data);
        $("#details_popup").modal("show");
    }

    // details click - to load popup on catalogue
    $("a.btn-info").on("click", (e) => {
        $("#results").text("");
        let id = e.target.dataset.id;
        let data = JSON.parse(e.target.dataset.details); // it's a string need an object
        CopyToModal(id, data);
    });
});


// populate the modal fields
const CopyToModal = (id, data) => {
    $("#qty").val("0");
    $("#cpu").text(data.CPU);
    $("#gpu").text(data.GPU);
    $("#ram").text(data.RAM);
    $("#ssd").text(data.SSD);
    $("#description").text(data.Description);
    $("#pname").text(data.PNAME);
    $("#msrp").text(cur(data.MSRP));
    $("#detailsGraphic").attr("src", "/images/" + data.GNAME);
    $("#selectedId").val(id);
}

