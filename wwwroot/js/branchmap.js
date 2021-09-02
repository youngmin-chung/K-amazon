// modal component
Vue.component("branchmodal", {
    template: "#modal-template",
    props: {
        details: [],
        lat: '',
        lng: ''
    },
    methods: {
        async showModal() {
            let details;
            try {
                this.modalStatus = "Getting Map...";
                let response = await fetch(`/GetBranches/${this.lat}/${this.lng}`);
                console.log(response);
                details = await response.json();
                //console.log(details);
                let myLatLng = new google.maps.LatLng(this.lat, this.lng);
                //console.log(myLatLng);
                let map_canvas = document.getElementById("map");
                let options = {
                    zoom: 10, center: myLatLng, mapTypeId:
                        google.maps.MapTypeId.ROADMAP
                };
                let map = new google.maps.Map(map_canvas, options);
                let center = map.getCenter();
                let infowindow = null;
                infowindow = new google.maps.InfoWindow({ content: "" });
                let idx = 0;
                details.map((detail) => {
                    idx = idx + 1;
                    let marker = new google.maps.Marker({
                        position: new google.maps.LatLng(detail.latitude, detail.longitude),
                        map: map,
                        animation: google.maps.Animation.DROP,
                        icon: `../images/marker${idx}.png`,
                        title: `Store#${detail.id} ${detail.street}, ${detail.city},${detail.region}`,
                        html: `<div>Store#${detail.id}<br/>${detail.street}, ${detail.city}<br/>${detail.distance.toFixed(2)} km</div>`
                    });
                    google.maps.event.addListener(marker, "click", () => {
                        infowindow.setContent(marker.html); // added .html to the marker object.
                        infowindow.close();
                        infowindow.open(map, marker);
                    });
                });
                map.setCenter(center);
                google.maps.event.trigger(map, "resize");
            } catch (error) {
                console.log(error.statusText);
            }
        }
    },
    mounted() {
        this.showModal();
    }
});


// Vue instance using google maps geocoder
const branches = new Vue({
    el: '#branches',
    methods: {
        loadAndShowModal() {
            let geocoder = new google.maps.Geocoder(); // A service for converting between an address to LatLng
            
            geocoder.geocode({ address: this.address }, (results, status) => {
                if (status === google.maps.GeocoderStatus.OK) { // only if google gives us the OK
                    this.lat = results[0].geometry.location.lat();
           
                    this.lng = results[0].geometry.location.lng();

                    this.showModal = true;
                }
            });
        }
    },
    data: {
        address: 'N5Y-5R6',
        lat: '',
        lng: '',
        showModal: false
    }
});