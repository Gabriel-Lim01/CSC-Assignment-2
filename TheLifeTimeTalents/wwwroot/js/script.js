$('#search').keyup(function () {
    //get data from json file
    var urlForJson = "data.json";


    //get data from Restful web Service in development environment
    //var urlForJson = "http://localhost:9000/api/talents";

    //get data from Restful web Service in production environment
    //var urlForJson= "http://csc123.azurewebsites.net/api/talents";

    //Url for the Cloud image hosting
    var urlForCloudImage = "http://res.cloudinary.com/doh5kivfn/image/upload/v1460006156/talents/";

    var searchField = $('#search').val();
    var myExp = new RegExp(searchField, "i");
    $.getJSON(urlForJson, function (data) {
        var output = '<ul class="searchresults">';
        $.each(data, function (key, val) {
            //for debug
            console.log(data);
            if ((val.Name.search(myExp) != -1) ||
                (val.Bio.search(myExp) != -1)) {
                output += '<li>';
                output += '<h2>' + val.Name + '</h2>';
                //get the absolute path for local image
                //output += '<img src="images/'+ val.ShortName +'_tn.jpg" alt="'+ val.Name +'" />';

                //get the image from cloud hosting
                //output += '<img src=' + urlForCloudImage + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                output += '<img src=' + 'https://talents-search.s3.amazonaws.com/' + val.ShortName + "_tn.jpg alt=" + val.Name + '" />';
                output += '<p>' + val.Bio + '</p>';
                output += '</li>';
            }
        });
        output += '</ul>';
        $('#update').html(output);
    }); //get JSON
});

$("#submit").click(function (e) {
    e.preventDefault();

    var longurl = $("#longurl").val();

    /* access token commented out for security reasons */
    var token = "083671bc4eabaaa42d846bd68325d1ab930e7936";

    var params = {
        "long_url": longurl
    };

    $.ajax({
        type: "POST",
        url: "https://api-ssl.bitly.com/v4/shorten",
        beforeSend: function (xhr) {
            /* Authorization header */
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(params)
    }).done(function (data) {
        $("#shorturl").html(data.link);
        console.log(data.link);

    }).fail(function (data) {
        console.log("Fail " + JSON.stringify(data));
    });


});
