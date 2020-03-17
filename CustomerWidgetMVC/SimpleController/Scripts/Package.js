$(document).ready(function () {
    //alert("fn called");
    $.ajax({
        url: 'http://localhost:59699/api/ServicePackage',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var id = new Array(data.length);
            var name = new Array(data.length);
            for (var i = 0; i < data.length; i++) {
                id[i] = data[i].ServiceId;
                name[i] = data[i].ServiceName;
                console.log(id[i] + ":" + name[i])
            }
            // console.log(name[0]);
            $('#spid1').val(id[0]);
            $('#spid2').val(id[1]);
            $('#spid3').val(id[2]);
            $('#sp1').append(name[0]);
            $('#sp2').append(name[1]);
            $('#sp3').append(name[2]);
            //document.getElementById("bsp").value = name[0];
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error in Operation');
            alert("error");
        }
    });//ajax
    var val;
    $("#basic").click(function () {
        val = document.getElementById('spid1').value;
        //alert(val);
        //alert("basic");
        window.location.href = "../Customer/Dealer";
    });
    $("#premium").click(function () {
        val = document.getElementById('spid2').value;
        //alert(val);
        //alert("Premium");
        window.location.href = "../Customer/Dealer";
    });
    $("#executive").click(function () {
        val = document.getElementById('spid3').value;
        //alert(val);
        //alert("Executive");
        window.location.href = "../Customer/Dealer";
    });

});
