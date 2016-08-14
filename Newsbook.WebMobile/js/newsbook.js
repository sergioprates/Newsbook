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