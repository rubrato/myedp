import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

var mymap = new L.Map('mapid').setView([-20.308811, -40.321348], 13);

L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
    maxZoom: 18,
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
        '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
        'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    id: 'mapbox.streets'
}).addTo(mymap);

L.marker([-20.308811, -40.321348]).addTo(mymap);


export class MapComponent {
    connection;
    constructor() {
        const builder = new HubConnectionBuilder();
        this.connection = HubConnection.builder.withUrl('http://localhost:5001/marcador').build();
    };

    ngOnInit() {
        this.connection.start()
            .them(() => {
                this.connection.on('getMarcador', (ll) => {
                    L.marker(ll).addTo(mymap);
                });

                this.connection.invoke('GetMarcador ');
            });
    }
}



var popup = L.popup();

function onMapClick(e) {
    popup
        .setLatLng(e.latlng)
        .setContent("You clicked the map at " + e.latlng)
        .openOn(mymap);
    L.marker(e.latlng).addTo(mymap);
}
mymap.on('click', onMapClick);
