$(function () {

    window.map = undefined;
   // markers = undefined;

    // Initialize Select2 select box
    $("select[name=\"validation-select2\"]").select2({
        allowClear: true,
        placeholder: "Choose a station . . . ",
    }).change(function () {
        $(this).valid();
    });

    // Trigger validation on tagsinput change
    $("input[name=\"validation-bs-tagsinput\"]").on("itemAdded itemRemoved", function () {
        $(this).valid();
    });

    // Initialize validation
    $("#validation-form").validate({
        ignore: ".ignore, .select2-input",
        focusInvalid: false,
        rules: {
            "validation-select2": {
                required: false
            }
        },
        // Errors
        errorPlacement: function errorPlacement(error, element) {
            var $parent = $(element).parents(".form-group");
            // Do not duplicate errors
            if ($parent.find(".jquery-validation-error").length) {
                return;
            }
            $parent.append(
                error.addClass("jquery-validation-error small form-text invalid-feedback")
            );
        },
        highlight: function (element) {
            var $el = $(element);
            var $parent = $el.parents(".form-group");
            $el.addClass("is-invalid");
            // Select2 and Tagsinput
            if ($el.hasClass("select2-hidden-accessible") || $el.attr("data-role") === "tagsinput") {
                $el.parent().addClass("is-invalid");
            }
        },
        unhighlight: function (element) {
            $(element).parents(".form-group").find(".is-invalid").removeClass("is-invalid");
        }
    });

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }


    // ===================================================================================================================================================
    // ===================================================================================================================================================

    setInterval(function () {

        //$('#pageClock').html(moment($('#pageClock').html()).add(3, 'seconds').format("YYYY-MM-DDTHH:mm:ss.SSSS"));
        //$('#pageClock').html(moment($('#pageClock').html(), "YYYY-MM-DD HH:mm:ss.SS").add(3, 'seconds').format("YYYY-MM-DD HH:mm:ss.SS"));
        $('#pageClock').html(moment.parseZone($('#pageClock').html()).add(3, 'seconds').format("YYYY-MM-DDTHH:mm:ssZ"));
        

    }, 3000); // 3 seconds

    setInterval(function () {

        console.log("isvisisble" + $('#divtable').is(':visible'));

        if ($('#divtable').is(':visible'))
        {

            $(".sHolder").each(function () { updateDelay(this); });

        }
    }, 60000); //60 secs

    setInterval(function () {

        $(".sRouteDelay").each(function () { updateDelay(this); });

    }, 60000);

    setInterval(function () {

        $(".routeTimeFuture").each(function () { updateStopArrivalDisplay(this); });

    }, 10000);

    setInterval(function () {

        if ($('#pageData').length) {
            renderVehiclePosition()
        }
    }, 30000); //30 secs

    // ===================================================================================================================================================
    // ===================================================================================================================================================

    function initializeMap(lat, lon, shape_coordinates, map_zoom, stop_data, stop_selected) {

        if (!window.map) {

            var osmUrl = 'https://api.mapbox.com/styles/v1/kikosantos/ck8vhgso31g5d1ip8cfm6rp8q/tiles/{z}/{x}/{y}?access_token=pk.eyJ1Ijoia2lrb3NhbnRvcyIsImEiOiJjazdwcmtkNWYwMGxtM21zNWszMWh5dG1lIn0.FrDqgayQLtzhy6FWG6E2Rw';
            var osmAttrib = 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>';
            var osm = new L.TileLayer(osmUrl, {
                attribution: osmAttrib,
                id: 'mapbox/streets-v11',
                tileSize: 512,
                zoomOffset: -1,
                accessToken: 'pk.eyJ1Ijoia2lrb3NhbnRvcyIsImEiOiJjazdwcmtkNWYwMGxtM21zNWszMWh5dG1lIn0.FrDqgayQLtzhy6FWG6E2Rw'
            });

            window.map = new L.Map('mapid', { layers: [osm], center: new L.LatLng(lat, lon), zoom: map_zoom });
            window.map.createPane('vehiclePane');
            //window.map.createPane('markerPane');
        }

        clearMap();
        var polyline = L.polyline(shape_coordinates, { color: '#5177FF', weight: 5, opacity: 1 }).addTo(window.map);

        renderStopMarkers(stop_data, stop_selected, map_zoom);

        window.map.setView([lat, lon], map_zoom);
    }

    function renderRoute(route_id, stop_name, lat, lon, shape_coordinates, stop_data) {

        var stationName = route_id.substring(3) + " - " + stop_name;

        $('#spnStationName').html(stationName);

        initializeMap(lat, lon, shape_coordinates, 14, stop_data, stop_name);

        //mapShowStop(lat, lon, stop_name, 14);
    }

    function renderVehiclePosition() {

        var data = JSON.parse($('#pageData').html());

        //console.log("clock = " + $('#pageClock').html());

        var dateTimeNow1 = moment.parseZone($('#pageClock').html())
        var dateTimeNow2 = moment.parseZone($('#pageClock').html())
        var timestamp_from = dateTimeNow1.add(-5, 'minutes').unix()
        //console.log("timestamp_from = " + timestamp_from);
        var timestamp_to = dateTimeNow2.unix();
        //console.log("timestamp_to = " + timestamp_to);

        var uri = "https://komyuterrealtime.azurewebsites.net/api/NaviRTVehiclePosition"
            + "?route_id=" + data.SelectedRouteId
            + "&trip_id=" + data.SelectedTripId
            + "&direction_id=" + data.SelectedDirectionId
            + "&trip_date=" + data.SelectedTripDate
            + "&trip_time=" + data.SelectedTripTime
            + "&timestamp_from=" + timestamp_from
            + "&timestamp_to=" + timestamp_to;

       //console.log("uri = " + uri);

        $.getJSON(uri)
            .done(function (data) {

                window.map.eachLayer(function (layer) {
                    if (layer.options && layer.options.pane === "vehiclePane") {
                        window.map.removeLayer(layer);
                    }
                });

                $.each(data, function () {
                    //console.log("this = " + JSON.stringify(this))

                    var location = [this.latitude, this.longitude];

                    var myIcon = L.icon({
                        iconUrl: 'img/vehiclemarker.png',
                        iconSize: [36, 59],
                        iconAnchor: [18, 50],
                        popupAnchor: [0, -18]
                    });
                    L.marker(location, { icon: myIcon, pane: "vehiclePane" }).addTo(window.map);
                    //L.marker(location, { icon: myIcon, pane: "vehiclePane" }).bindTooltip('A sweet static label!', { noHide: true })
                    //    .addTo(window.map)
                    //    .openTooltip();



                    return;
                });
            });
    }

    function renderStopMarkers(stop_data, selected_stop, zoom) {

        if (stop_data.length > 0) {

            // delete marker pane
            window.map.eachLayer(function (layer) {
                if (layer.options && layer.options.pane === "markerPane") {
                    window.map.removeLayer(layer);
                }
            });

            var stationMarker = L.icon({
                iconUrl: 'img/stationmarker.png',
                iconSize: [17, 17],
                iconAnchor: [8, 8],
                tooltipAnchor: [10, 0]
            });

            var zoomLocation = [];

            for (var i = 0; i < stop_data.length; i++) {

                if (stop_data[i].stop_name == selected_stop) {
                    zoomLocation = [stop_data[i].lat, stop_data[i].lng];
                    class_name = "tooltipSelected";
                }
                else
                    class_name = "";

                addStopMarker(stationMarker, [stop_data[i].lat, stop_data[i].lng], stop_data[i].stop_name, class_name);
            }

            if (zoomLocation.length > 0)
                window.map.setView(zoomLocation, zoom);
        }

    }

    function addStopMarker(stationMarker, location, label, class_name) {

        L.marker(location, { icon: stationMarker, pane: "markerPane" }).bindTooltip(label, { noHide: true, permanent: true, className: class_name })
            .addTo(window.map)
            .openTooltip();
    }

    function mapShowRoute(routeId, center_lat, center_lon, default_zoom, stop_data) {
        //console.log("center_lat = " + center_lat);
        //console.log("center_lon = " + center_lon);
        //console.log("default_zoom = " + default_zoom);

        var uri = "https://komyuterrealtime.azurewebsites.net/api/Shape?route_id=" + routeId;
        $.getJSON(uri)
            .done(function (data) {
                var route_coordinates = [];

                $.each(data, function () {
                    route_coordinates.push({
                        lat: this.shape_pt_lat,
                        lng: this.shape_pt_lon
                    })
                    //route_coordinates.push([this.shape_pt_lat, this.shape_pt_lon]);
                });

                initializeMap(center_lat, center_lon, route_coordinates, default_zoom, stop_data, "");
            });
    }

    function clearMap() {

        window.map.eachLayer(function (layer) {

            //console.log("layer.options.pane = " + layer.options.pane);
            //console.log("layer.options._path = " + layer.options._path);

            if (layer.options &&
                (layer.options.pane === "overlayPane" || layer.options.pane === "markerPane" || layer.options.pane === "tooltipPane") ) {
                window.map.removeLayer(layer);
            }
        });


        //for (i in window.map._layers) {
        //    if (window.map._layers[i]._path != undefined) {
        //        try {
        //            //if (window.map._layers[i].options.pane != "vehiclePane")
        //            console.log("window.map._layers[i].options.pane = " + window.map._layers[i].options.pane);
        //            window.map.removeLayer(window.map._layers[i]);


        //        }
        //        catch (e) {
        //            console.log("problem with " + e + m._layers[i]);
        //        }
        //    }
        //}


    }

    function mapShowStop(lat, lon, station, zoom) {
        //console.log("lat = " + lat);
        //console.log("lon = " + lon);
        //console.log("station = " + station);

        //var markerPosition = { lat: lat, lng: lon };
        var markerPosition = [lat, lon];
        //clearStopMarkers();
        addStopMarker(markerPosition, station, zoom);
        //setMapOnAll(window.map);


    }

    function clearStopMarkers() {
        setMapOnAll(null);
        markers = [];
    }

    // Sets the map on all markers in the array.
    function setMapOnAll(map) {
        for (var i = 0; i < markers.length; i++) {
            markers[i].setMap(map);
        }
    }

    // ===================================================================================================================================================
    // ===================================================================================================================================================

    function updateDelay(obj)
    {
        var curMinStr = $(obj).children('span:first-child').html();
        console.log("curMinStr = " + curMinStr);

        if (curMinStr == "&lt;1")
            $(obj).remove();
        else {
            var curMin = parseFloat(curMinStr);
            var remMin = curMin - 1;

            if (remMin == 0)
                $(obj).children('span:first-child').html("<1");
            else
                $(obj).children('span:first-child').html(remMin + "");


            if (remMin < 2)
                $(obj).children('span:nth-child(2)').html(" min");
        }
    }

    function updateStopArrivalDisplay(obj) {

        var dateTimeNow = moment.parseZone($('#pageClock').html())
        var stopTime = $(obj).html();

        var nowTimeObj = moment(dateTimeNow.format("h:mma"), "h:mma");
        var stopTimeObj = moment(stopTime, "h:mma");

        //console.log("nowTimeObj = " + nowTimeObj + "; stopTimeObj = " + stopTimeObj);

        if (stopTimeObj < nowTimeObj)
            $(obj).attr('class', 'routeTimePast');

    }

    // =======================================================================================

    $(document).ready(function () {

        if ($('#pageData').length) {

            var dataJson = JSON.parse($('#pageData').html());

            // route page
            // ===================================================
            renderRoute(dataJson.SelectedRouteId, dataJson.SelectedStopName, dataJson.SelectedStopLat, dataJson.SelectedStopLon, dataJson.ShapeCoordinates, dataJson.StopData);

            console.log("$('.stopmaker').length = " + $('.stopmarker').length)

            adjustTableHeight("route", $('.stopmarker').length, 50);

            renderVehiclePosition();
        }
        else {
            // stop/index page ===================================
            // ===================================================
            var urlVars = getUrlVars();
            console.log("urlVars = " + JSON.stringify(urlVars));

            var route_id = "ROULRT1";

            if (urlVars.route_id && ("ROULRT1|ROULRT2|ROUMRT3|ROUPNR".indexOf(urlVars.route_id) > -1))
                route_id = urlVars.route_id;

            changeRoute(route_id);
        }

    });

    $('#btnLrt1').click(function () {
        changeRoute($(this).attr("value"));
    });
    $('#btnLrt2').click(function () {
        changeRoute($(this).attr("value"));
    });
    $('#btnMrt3').click(function () {
        changeRoute($(this).attr("value"));
    });
    $('#btnPnr').click(function () {
        changeRoute($(this).attr("value"));
    });

    $("button[name='routebtn']").click(function () {
        window.location.href = "/?route_id=" + $(this).val();
    });

    $(".routestop").click(function () {
        //Do stuff when clicked
        var rowValue = $(this).attr("value");
        var rowValueArr = rowValue.split("_");

        var dataJson = JSON.parse($('#pageData').html());

        $('#pageDataSelectedStopId').html(rowValueArr[3]);

        renderRoute(dataJson.SelectedRouteId, rowValueArr[0], rowValueArr[1], rowValueArr[2], dataJson.ShapeCoordinates, dataJson.StopData);
        
        changeStopIcon(parseInt(rowValueArr[4]), parseInt($('#pageDataSelectedStopRow').html()), parseInt(rowValueArr[5]))

        adjustTableHeight("route", parseInt(rowValueArr[5]), 50);
    });

    function changeStopIcon(row_new, row_old, max_row) {

        var imageNew = "";
        var imageOld = "";

        switch (row_new) {
            case 1:
                imageNew = "img/stop_on_end_top.png";
                break;
            case max_row:
                imageNew = "img/stop_on_end_bottom.png";
                break;
            default:
                imageNew = "img/stop_on_mid.png";
                break;
        }

        switch (row_old) {
            case 1:
                imageOld = "img/stop_off_end_top.png";
                break;
            case max_row:
                imageOld = "img/stop_off_end_bottom.png";
                break;
            default:
                imageOld = "img/stop_off_mid.png";
                break;
        }

        $(".stopmarker").eq(row_new - 1).attr("src", imageNew);
        $(".stopmarker").eq(row_old - 1).attr("src", imageOld);

        $('#pageDataSelectedStopRow').html(row_new);
    }

    $("button[name='reversetripbutton']").click(function () {

        var data = JSON.parse($('#pageData').html());

        var dataArr = $(this).val().split("_");
        var url = "Route"
            + "?route_id=" + data.SelectedRouteId
            + "&trip_id=" + dataArr[0]
            + "&stop_id=" + $('#pageDataSelectedStopId').html()
            + "&trip_date=" + data.SelectedTripDate
            + "&trip_time=" + dataArr[1]
            + "&direction_id=" + dataArr[2];
        console.log(url);

        window.location.href = url;
    });

    $("button[name='prevnextbutton']").click(function () {

        var data = JSON.parse($('#pageData').html());
        var trip_time = $(this).val();//.split("_");

        var url = "Route"
            + "?route_id=" + data.SelectedRouteId
            + "&trip_id=" + data.SelectedTripId
            + "&stop_id=" + $('#pageDataSelectedStopId').html()
            + "&trip_date=" + data.SelectedTripDate
            + "&trip_time=" + trip_time
            + "&direction_id=" + data.SelectedDirectionId;
        console.log(url);

        window.location.href = url;
    });

    $(".folderimg").click(function () {

        var imgsrc = $(".folderimg").attr('src');

        if (imgsrc == "/img/tbl_collapse.png") {
            imgsrc = "/img/tbl_expand.png";
            $('#divtable').hide();
        }
        else {
            imgsrc = "/img/tbl_collapse.png";
            $('#divtable').show();
        }

        $(".folderimg").attr("src", imgsrc);
    });

    $('#ddLStation').change(function () {
        $(this).find(":selected").each(function ()
        {
            var ddValue = $(this).val();

            if (ddValue)
            {
                console.log("ddValueArr = " + ddValue);
                var ddValueArr = ddValue.split("_");
                var rowCounter = 0;
                console.log("ddValueArr[1] = " + ddValueArr[1]);

                //<span id="localTime">2020-04-10T11:15:05.9504820Z</span>
                var localDateTime = $('#pageClock').html()
                console.log("localDateTime = " + localDateTime);

                var schedCount = 8;

                var uri = "https://komyuterrealtime.azurewebsites.net/api/Frequency?"
                    + "route_id=" + ddValueArr[0]
                    + "&stop_id=" + ddValueArr[1]
                    + "&travel_datetime=" + moment.parseZone(localDateTime).format("YYYY-MM-DD HH:mm:ss")
                    + "&schedule_count=" + schedCount;

                $.getJSON(uri)
                    .done(function (data)
                    {
                        var stopName = "";

                        $("#tblStops > tbody").html("");

                        $.each(data, function () {

                            if (this.stop_name != this.trip_headsign) {

                                stopName = this.stop_name;
                                var strArrrivalTimeRounded = "No Service";
                                var delayHtml = "";//"<span class='sHolder'><span>" + this.id + "</span> mins <img class='imgwifi' src='img/wifi.gif' /></span>"

                                var directionId = this.direction_id;
                                var tripId = this.trip_id;
                                var tripStartTime = this.start_time;
                                var rt_delay = this.rt_delay;


                                console.log("this.arrival_time = " + this.arrival_time)
                                console.log("this.rt_delay = " + this.rt_delay)

                                //if (parseInt(this.headway_secs) != 10000) {
                                //if (parseInt(this.headway_secs) > 0) {

                                var strArrivalTime = moment(this.arrival_time, "HH:mm:ss");//.add(this.rt_delay, 'seconds');

                                if (strArrivalTime.seconds() == 0)
                                    strArrrivalTimeRounded = strArrivalTime.format("h:mma");
                                else
                                    strArrrivalTimeRounded = strArrivalTime.add(60 - (strArrivalTime.seconds() % 60), 'seconds').format("h:mma");

                                if (this.rt_delay > 0) {

                                    var phTimeStr = moment.parseZone(localDateTime).format("HH:mm:ss")
                                    var strDelay = strArrivalTime.diff(moment(phTimeStr, "HH:mm:ss"), 'minutes');

                                    console.log("actualarrivaltime = " + strArrivalTime.format("HH:mm:ss"));
                                    console.log("phTimeStr = " + phTimeStr);
                                    console.log("strDelay = " + strDelay)

                                    if (strDelay > 0) {
                                        var strMin = (strDelay > 1) ? "mins" : "min";

                                        delayHtml = "<span class='sHolder'><span>" + strDelay + "</span> <span>" + strMin + "</span> <img class='imgwifi' src='img/wifi.gif' /></span>"
                                    }
                                }

                                console.log("delayHtml = " + delayHtml);

                                var rowHref = "/Route?route_id=" + ddValueArr[0]
                                    + "&trip_id=" + this.trip_id
                                    + "&stop_id=" + ddValueArr[1]
                                    + "&trip_date=" + moment(localDateTime, "YYYY-MM-DD HH:mm:ss.SS").format("YYYYMMDD")
                                    + "&trip_time=" + this.start_time
                                    + "&direction_id=" + this.direction_id;

                                var boundStr = "<span class='text-info'> [Southbound]</span>";

                                if (ddValueArr[0] == "ROULRT2") {
                                    if (this.direction_id == "1")
                                        boundStr = "<span class='text-warning'> [Westbound]</span>";
                                    else
                                        boundStr = "<span class='text-info'> [Eastbound]</span>";
                                }
                                else {
                                    if (this.direction_id == "1")
                                        boundStr = "<span class='text-warning'> [Northbound]</span>";
                                }

                                rowCounter++;
                                $("#tblStops").append("<tr><td>"
                                    + "<a href='" + rowHref + "' style='font-weight: bold;'>" + stopName + "</a>"
                                    + "<div style='padding-left: 15px;'>to " + this.trip_headsign
                                    + boundStr
                                    + "</div></td><td style='text-align: right;'>" + strArrrivalTimeRounded + "<br />"
                                    + delayHtml
                                    + "&nbsp;</td></tr>");

                            }
                        });

                        if (rowCounter > 0) {
                            $('#alertChooseStation').hide();
                            $('#divtable').show();
                            $('.tablefolder').show();

                            adjustTableHeight("stop", rowCounter, 70);

                            // reload map
                            //mapShowStop(parseFloat(ddValueArr[2]), parseFloat(ddValueArr[3]), stopName, 16);
                            var stopData = JSON.parse($('#stopData').html())
                            renderStopMarkers(stopData, stopName, 16)
                        }
                    });


                serviceAlert_Reset();
                serviceAlert_Render(ddValueArr[1])
            }
        });
    });

    function adjustTableHeight(page, totalRow, rowHeight) {

        var adjustmentHeight = 0;

        if (page == "route") {
            adjustmentHeight = 185;
        }
        else// if (page == "stop")
        {
            if ($('#alertService').is(":visible"))
                adjustmentHeight = 180;
            else
                adjustmentHeight = 130;
        }

        var folderHeight = 14;

        //console.log("page = " + page);
        //console.log("$(window).height() = " + $(window).height());
        //console.log("adjustmentHeight = " + adjustmentHeight);
        //console.log("folderHeight = " + folderHeight);

        var tableTrueHeight = (rowHeight * totalRow);
        var tableMaxHeight = ($(window).height() - adjustmentHeight - folderHeight);
        var tableAdjustedHeight = tableTrueHeight;

        //console.log("tableAdjustedHeight = " + tableAdjustedHeight);
        //console.log("tableMaxHeight = " + tableMaxHeight);
        //console.log("tableTrueHeight = " + tableTrueHeight);

        if (tableAdjustedHeight > tableMaxHeight) {
            $('#divtable').height(tableMaxHeight);
        }
    }

    function changeRoute(id)
    {
        var default_zoom = "";
        var center_lat = "";
        var center_lon = "";
        var route_coordinates = []

        $('#btnLrt1').prop('disabled', ('ROULRT1' == id));
        $('#btnLrt2').prop('disabled', ('ROULRT2' == id));
        $('#btnMrt3').prop('disabled', ('ROUMRT3' == id));
        $('#btnPnr').prop('disabled', ('ROUPNR' == id));


        $('#alertChooseStation').show();
        $('#divtable').hide();
        $('.tablefolder').hide();

        // populate dropdwon
        var $dropdown = $("#ddLStation");
        var uri = "https://komyuterrealtime.azurewebsites.net/api/Stop?route_id=" + id;
        $.getJSON(uri)
            .done(function (data)
            {
                $dropdown
                    .empty()
                    .append('<option value>Choose a station...</option>');

                //console.log("data.length = " + data.length);
                var station_count = data.length;

                var stop_data = [];

                $.each(data, function ()
                {
                    stop_data.push({
                        stop_id: this.stop_id,
                        stop_name: this.stop_name,
                        stop_sequence: this.stop_sequence,
                        lat: this.stop_lat,
                        lng: this.stop_lon

                    });

                    var ddValue = id + "_" + this.stop_id + "_" + this.stop_lat + "_" + this.stop_lon + "_" + this.stop_sequence + "_" + station_count;
                    //console.log(this);
                    $dropdown.append($("<option />").val(ddValue).text(this.stop_name));

                    default_zoom = this.default_zoom;
                    center_lat = this.center_lat;
                    center_lon = this.center_lon;
                });

                $('#stopData').html(JSON.stringify(stop_data));

                // show centered map
                mapShowRoute(id, center_lat, center_lon, default_zoom, stop_data);
            });

        // get service alert data
        var uri = "https://komyuterrealtime.azurewebsites.net/api/NaviRTServiceAlert?route_id=" + id;
        $.getJSON(uri)
            .done(function (data) {

                $('#serviceAlertData').html(JSON.stringify(data));

                serviceAlert_Reset();
                serviceAlert_Render("")

            });
    }

    function serviceAlert_Reset() {

        $('#alertService').hide();
        $('#divAlertContainer').html('');

    }

    function serviceAlert_Render(stop_id) {

        var serviceAlertData = JSON.parse($('#serviceAlertData').html());
        console.log("serviceAlertData = " + JSON.stringify(serviceAlertData));

        var alertContentHtml = "";
        var alertModalHtml = "";

        for (var i = 0; i < serviceAlertData.length; i++) {

            if (serviceAlertData[i].stop_id == null || serviceAlertData[i].stop_id == "" || serviceAlertData[i].stop_id == stop_id) {

                var saId = serviceAlertData[i].id;
                var saHeader = serviceAlertData[i].header;
                var saStart = moment(serviceAlertData[i].start_date).format("M/DD");
                var saEnd = moment(serviceAlertData[i].end_date).format("M/DD");

                if (alertContentHtml != "")
                    alertContentHtml += "<br />";

                alertContentHtml += "<a href=\"#\" data-toggle=\"modal\" data-target=\"#centeredModalWarning_" + saId + "\">" + saHeader + "</a> - " + saStart + " to " + saEnd;

                alertModalHtml += serviceAlert_GenerateModal(saId, saHeader, serviceAlertData[i].description, serviceAlertData[i].start_date, serviceAlertData[i].end_date);
            }

        }

        console.log("alertContentHtml = " + alertContentHtml);

        if (alertContentHtml == "")
            serviceAlert_Reset()
        else {

            $('#alertService').find('.alert-message').html(alertContentHtml);
            $('#divAlertContainer').html(alertModalHtml);

            $('#alertService').show();
        }

    }

    function serviceAlert_RenderModal() {

        $('#divAlertContainer').html('');
        
    }

    function serviceAlert_GenerateModal(id, header, description, start_date, end_date) {

        var html = "";
        html += "<div class=\"modal fade\" id=\"centeredModalWarning_" + id  + "\" tabindex=\"-1\" role=\"dialog\" aria-hidden=\"true\">";
        html += "    <div class=\"modal-dialog modal-dialog-centered\" role=\"document\">";
        html += "        <div class=\"modal-content\">";
        html += "            <div class=\"modal-header\">";
        html += "                <h5 class=\"modal-title\">" + header + "</h5>";
        html += "                <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">";
        html += "                    <span aria-hidden=\"true\">&times;</span>";
        html += "                </button>";
        html += "            </div>";
        html += "            <div class=\"modal-body m-3\">";
        html += "                <p class=\"mb-0\">";
        html += "                    <span class=\"font-weight-bolder\">Duration:</span> " + moment(start_date).format("M/DD/YYYY") + " to " + moment(end_date).format("M/DD/YYYY");
        html += "                    <br /><br />" + description + "";
        html += "                </p>";
        html += "            </div>";
        html += "            <div class=\"modal-footer\">";
        html += "                <button type=\"button\" class=\"btn btn-warning\" data-dismiss=\"modal\">Close</button>";
        html += "            </div>";
        html += "        </div>";
        html += "    </div>";
        html += "</div>";

        return html;
    }
});