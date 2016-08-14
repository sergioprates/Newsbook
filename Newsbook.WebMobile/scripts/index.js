/// <reference path="../lib/js/vue.js" />

new Vue({
    // We want to target the div with an id of 'events'
    el: '#paginaPrincipal',

    // Here we can register any values or collections that hold data
    // for the application
    data: {
        ListaFeedUrl: [],
        Token: '',
        ListaNoticia: []
    },

    // Anything within the ready function will run when the application loads
    ready: function () {
        this.buscarToken();
    },

    // Methods we want to use in our application are registered here
    methods: {
        listarFeedUrlAtivo: function () {
            //this.$set('events', events);
            this.$http.get(_urlapi + 'feedurl', {headers: { 'Authorization': 'Bearer ' + this.Token}}).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                this.$set('ListaFeedUrl', resposta.Result.d);
            }, function (error)  {
                // error callback
                mostrarErro(error);
            });

        },
        listarNoticia: function () {
            this.$http.get(_urlapi + 'noticia/hoje', { headers: { 'Authorization': 'Bearer ' + this.Token } }).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                this.$set('ListaNoticia', resposta.Result.d);
                ocultarAguarde();
            }, function (error) {
                // error callback
                mostrarErro(error);
            });
        },
        listarNoticiaPorFeedUrl: function (id) {
            mostrarAguarde();
            this.$http.get(_urlapi + 'noticiadofeedurl/hoje/' + id, { headers: { 'Authorization': 'Bearer ' + this.Token } }).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                this.$set('ListaNoticia', resposta.Result.d);
                ocultarAguarde();
            }, function (error) {
                // error callback
                mostrarErro(error);
            });

            return false;
        },
        buscarToken: function () {
            mostrarAguarde();
            if (this.Token == '') {
                this.$http.post(_urltoken, autenticacao, { headers: { 'Content-Type': 'Application/x-www-form-urlencoded' } }).then(function (res) {
                    // success callback
                    var resposta = JSON.parse(res.body);

                    //this.$set('ListaFeedUrl', resposta.Result.d);
                    this.$set('Token', resposta.access_token);
                    this.listarFeedUrlAtivo();
                    this.listarNoticia();
                }, function (error) {
                    // error callback
                    mostrarErro(error);
                });
            }
            else
            {
                this.listarFeedUrlAtivo();
                this.listarNoticia();
            }
            
        }
    }
});

