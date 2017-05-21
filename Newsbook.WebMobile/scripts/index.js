/// <reference path="../lib/js/vue.js" />



var index = new Vue({
    // We want to target the div with an id of 'events'
    el: '#paginaPrincipal',

    // Here we can register any values or collections that hold data
    // for the application
    data: {
        ListaFeedUrl: [],
        ListaNoticia: [],
        Token: '',
        Limit: 10
    },

    // Anything within the ready function will run when the application loads
    ready: function () {
        components.buscarToken(function (token) {
            index.Token = token;
            mostrarAguarde();
            index.listarFeedUrlAtivo(token);
            index.listarNoticia(token);
        });
    },

    // Methods we want to use in our application are registered here
    methods: {
        listarFeedUrlAtivo: function (token) {
            //this.$set('events', events);
            this.$http.get(_urlapi + 'feedurl', { headers: { 'Authorization': 'Bearer ' + this.Token}}).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                this.$set('ListaFeedUrl', resposta.Result.d);
            }, function (error)  {
                // error callback
                mostrarErro(error);
            });

        },
        listarNoticia: function (token) {
            this.$http.get(_urlapi + 'noticia?limit=' + this.Limit + '&skip=' + this.ListaNoticia.length, { headers: { 'Authorization': 'Bearer ' + this.Token } }).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                for (var i = 0; i < resposta.Result.d.length; i++) {
                    index.ListaNoticia.push(resposta.Result.d[i]);
                }

                ocultarAguarde();
                $('#carregandoNoticias').hide();
                this.$nextTick(function () {
                    $('img', $('#listaNoticia')).addClass('img-responsive');
                });
            }, function (error) {
                // error callback
                mostrarErro(error);
            });
        },
        listarNoticiaPorFeedUrl: function (id) {
            mostrarAguarde();
            this.$http.get(_urlapi + 'noticia/' + id, { headers: { 'Authorization': 'Bearer ' + this.Token } }).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                this.$set('ListaNoticia', resposta.Result.d);
                ocultarAguarde();

                this.$nextTick(function () {
                    $('img', $('#listaNoticia')).addClass('img-responsive');
                });
            }, function (error) {
                // error callback
                mostrarErro(error);
            });

            return false;
        }, 
        criarNovoFeed: function () {
            formFeed.Init(function (feed) {
                mostrarAguarde();
                this.$http.post(_urlapi + 'feedurl/criar?feed=' + encodeURIComponent(feed), {}, { headers: { 'Authorization': 'Bearer ' + index.Token } }).then(function (res) {
                    // success callback
                    
                    index.listarFeedUrlAtivo(index.Token);
                    index.listarNoticia(index.Token);
                    formFeed.Destroy();
                }, function (error) {
                    // error callback
                    mostrarErro(error);
                });
            });
        }
    }
});




$(document).ready(function () {
    var win = $(window);

    // Each time the user scrolls
    win.scroll(function () {
        // End of the document reached?
        if ($(document).height() - win.height() == win.scrollTop()) {
            $('#carregandoNoticias').show();
            index.listarNoticia();
        }
    });
});