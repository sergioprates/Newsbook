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
        Limit: 10,
        Request: false,
        FeedSelecionado: ''
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
            this.$http.get(_urlapi + 'feedurl', { headers: { 'Authorization': 'Bearer ' + this.Token } }).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                this.$set('ListaFeedUrl', resposta.Result.d);
            }, function (error) {
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
                index.Request = false;
                this.$nextTick(function () {
                    $('img', $('#listaNoticia')).addClass('img-responsive');
                });
            }, function (error) {
                index.Request = false;
                // error callback
                mostrarErro(error);
            });
        },
        listarTodos: function () {
            this.ListaNoticia = [];
            this.FeedSelecionado = '';
            mostrarAguarde();
            this.listarNoticia();
        },
        listarNoticiaPorFeedUrl: function (id, novo) {
           
            if (novo) {
                this.ListaNoticia = [];
                mostrarAguarde();
            }

            if (detectmob() == true) {
                $(".app-container").toggleClass("expanded", false);
                $(".navbar-expand-toggle").toggleClass("fa-rotate-90", false);
            }

            index.FeedSelecionado = id;

            this.$http.get(_urlapi + 'noticia/' + id + '?limit=' + this.Limit + '&skip=' + this.ListaNoticia.length, { headers: { 'Authorization': 'Bearer ' + this.Token } }).then(function (res) {
                // success callback
                var resposta = JSON.parse(res.body);

                var resposta = JSON.parse(res.body);

                for (var i = 0; i < resposta.Result.d.length; i++) {
                    index.ListaNoticia.push(resposta.Result.d[i]);
                }

                ocultarAguarde();
                $('#carregandoNoticias').hide();
                index.Request = false;
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


function detectmob() {
    if (window.innerWidth <= 800 && window.innerHeight <= 600) {
        return true;
    } else {
        return false;
    }
}




$(document).ready(function () {

    if (detectmob() == true) {
            $(".app-container").toggleClass("expanded", false);
            $(".navbar-expand-toggle").toggleClass("fa-rotate-90", false);
    }

    $(window).resize(function () {
        if (detectmob() == true) {
            $(".app-container").toggleClass("expanded", false);
            $(".navbar-expand-toggle").toggleClass("fa-rotate-90", false);
        }
    });

    $('#paginaPrincipal').fadeIn();
    var win = $(window);

    // Each time the user scrolls
    win.scroll(function () {
        // End of the document reached?
        if ($(document).height() - win.height() == win.scrollTop() && index.Request == false) {
            index.Request = true;
            $('#carregandoNoticias').show();

            if (index.FeedSelecionado == '') {
                index.listarNoticia();
            }
            else {
                index.listarNoticiaPorFeedUrl(index.FeedSelecionado, false);
            }
        }
    });
});

$(function () {

    var $contextMenu = $("#contextMenu");

    $("body").on("contextmenu", ".itemfeed", function (e) {

        //$('small', $contextMenu).html($(e.target).text());
        $contextMenu.css({
            display: "block",
            left: e.pageX,
            top: e.pageY
        });
        return false;
    });

    $contextMenu.on("click", "a", function () {
        $contextMenu.hide();
    });

    $('html').on("click", function () {
        $contextMenu.hide();
    });
});