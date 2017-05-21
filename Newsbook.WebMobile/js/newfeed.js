var formFeed = new Vue({
    // We want to target the div with an id of 'events'
    el: '#modalFeed',
    data: {
        feed: '',
        Callback: ''
    },
    methods: {
        Init: function (callback) {
            $('#modalFeed').modal('show');
            this.Callback = callback;
        },
        Destroy: function () {
            $('#modalFeed').modal('hide');
            this.feed = '';
        },
        Salvar: function () {
            if (this.feed == '') {
                $('#txtFeed').parent().addClass('has-error');
                return false;
            }
            else {
                $('#txtFeed').parent().removeClass('has-error');
                this.Callback(this.feed);
            }
        }
    }
});