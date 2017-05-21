function mostrarAguarde() {
    $('#modalAguarde').modal('show');
}

function ocultarAguarde() {
    $('#modalAguarde').modal('hide');
}

function mostrarErro(erro) {
    ocultarAguarde();
    console.log(erro);
}

function ativarMenuItem(item) {
    $($(item).parent().parent().children()).removeClass('active');
    $($(item).parent()).addClass('active');
}

var components = new Vue({
    data: {
        Token: ''
    }, 
    methods: {
        buscarToken: function (callback) {
            mostrarAguarde();
            if (this.Token == '') {
                this.$http.post(_urltoken, autenticacao, { headers: { 'Content-Type': 'Application/x-www-form-urlencoded' } }).then(function (res) {
                    // success callback
                    var resposta = JSON.parse(res.body);

                    //this.$set('ListaFeedUrl', resposta.Result.d);
                    this.Token = resposta.access_token;
                    this.$set('Token', resposta.access_token);
                    callback(resposta.access_token);
                }, function (error) {
                    // error callback
                    mostrarErro(error);
                });
            }
            else {
                this.listarFeedUrlAtivo();
                this.listarNoticia();
            }

        }
    }
});