var mymap = L.map('mapid').fitWorld();

mymap.locate({ setView: true, maxZoom: 16 });

var selectmark = L.icon({
    iconUrl: 'images/pinmark.png',

    iconSize: [20, 20], // size of the icon
    iconAnchor: [10, 20], // point of the icon which will correspond to marker's location
    popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
});

var prb1mark = L.icon({
    iconUrl: 'images/problm1.pg',

    iconSize: [20, 20], // size of the icon
    iconAnchor: [10, 20], // point of the icon which will correspond to marker's location
    popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
});

var prb2mark = L.icon({
    iconUrl: 'images/problm2.jpg',

    iconSize: [20, 20], // size of the icon
    iconAnchor: [10, 20], // point of the icon which will correspond to marker's location
    popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
});

var prb3mark = L.icon({
    iconUrl: 'images/problm3.jpg',

    iconSize: [20, 20], // size of the icon
    iconAnchor: [10, 20], // point of the icon which will correspond to marker's location
    popupAnchor: [-3, -76] // point from which the popup should open relative to the iconAnchor
});

L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
    maxZoom: 18,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
        '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
        'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    id: 'mapbox.streets'
}).addTo(mymap);

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/marcador")
    .configureLogging(signalR.LogLevel.Information)
    .build();
connection.start().catch(err => console.error(err.toString()));

connection.on("getMarcador", (lttd, lngtd) => {
    console.log(lttd, lngtd);
    L.marker([lttd, lngtd]).addTo(mymap);
});

connection.on("sendMarcador", (lttd, lngtd, obs) => {
    L.marker([lttd, lngtd]).addTo(mymap).bindPopup(obs);
});

connection.on("sendConsulta", (consulta) => {
    console.log(consulta);
});

connection.on("alert", (alerta) => {
    alert(alerta);
});

$(document).ready(function () {
    $(document).on('change', '#tipoproblema', function () {
        console.log($(this).val());

    });
});

var popup = L.popup();
var sndlttd;
var sndlngtd;
var theMarker = {};

function onMapClick(e) {
    //popup
    //    .setLatLng(e.latlng)
    //    .setContent("You clicked <br> the map at " + e.latlng.lat + ", " + e.latlng.lng)
    //    .openOn(mymap);
    sndlttd = e.latlng.lat;
    sndlngtd = e.latlng.lng;
    if (theMarker != undefined) {
        mymap.removeLayer(theMarker);
    };
    theMarker = L.marker([sndlttd, sndlngtd], { icon: selectmark }).addTo(mymap)
    //connection.invoke('GetMarcador', e.latlng.lat, e.latlng.lng);
}
mymap.on('click', onMapClick);


var botaoAdicionar = document.querySelector("#adicionar-servico")

botaoAdicionar.addEventListener("click", function () {
    event.preventDefault(); //evita a pagina ser recarregada ao clicar no botao
    mymap.removeLayer(theMarker);
    var form = document.querySelector("#form-adicionar");
    var cpf = String(form.cpf.value);
    var nome = String(form.nome.value);
    var telefone = String(form.telefone.value);
    var observacoes = String(form.observacoes.value);
    console.log(cpf + "<br>" + nome + "<br>" + telefone + "<br>" + observacoes);
    connection.invoke('SendMarcador', sndlttd, sndlngtd, cpf, nome, telefone, observacoes);
});