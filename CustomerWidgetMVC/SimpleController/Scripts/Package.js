$(document).ready(function () {
    //alert("fn called");
    debugger;
    $.ajax({
        url: 'http://localhost:59699/api/GetServicePackage',
        type: 'GET',
       // contentType: "application/text; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            console.log("............" + data + "...............");

            var serviceData = data;

            var id = new Array(serviceData.length);
            var name = new Array(serviceData.length);
            console.log(serviceData);
            for (var i = 0; i < serviceData.length; i++) {
                console.log( serviceData[i].ServiceId);
                console.log(serviceData[i].ServiceName);
                id[i] = serviceData[i].ServiceId;
                name[i] = serviceData[i].ServiceName;
            //    
            }
            //for (var i = 0; i < data.length; i++) {
            //    console.log(id[i] + ":" + name[i])
            //}
            // console.log(name[0]);
            $('#spid1').val(id[0]);
            $('#spid2').val(id[1]);
            $('#spid3').val(id[2]);
            $('#sp1').append(name[0]);
            $('#sp2').append(name[1]);
            $('#sp3').append(name[2]);
            $('#s1').val(name[0]);
            $('#s2').val(name[1]);
            $('#s3').val(name[2]);

            //document.getElementById("bsp").value = name[0];
        }
        ////error: function (xhr, textStatus, errorThrown) {
        ////    console.log('Error in Operation');
        ////    alert("error");
        ////}
    });//ajax
    var val;
    $("#basic").click(function () {
        val = document.getElementById('spid1').value;
       // session["PackageName"] = Pname;
       // console.log(session["PackageName"]);

        Pname = document.getElementById('s1').value;
     
        //alert("basic");
        window.location.href = "../Customer/Dealer?packageid=" + val+ "&type=" + Pname;

    });
    $("#premium").click(function () {
        val = document.getElementById('spid2').value;
        Pname = document.getElementById('s2').value;
        //alert(val);
        //alert("Premium");
        window.location.href = "../Customer/Dealer?packageid=" + val + "&type=" + Pname;
    });
    $("#executive").click(function () {
        val = document.getElementById('spid3').value;
        Pname = document.getElementById('s3').value;
        //alert(val);
        //alert("Executive");
        window.location.href = "../Customer/Dealer?packageid=" + val + "&type=" + Pname;
    });

});
