function RetornarTextoStatus(status) {
    if (status == 0) {
        return "Pendente de coleta no cliente";
    }
}

function AtualizarListaSolicitacoes(pagina) {
    var dataInicial = $("#data-inicial-entrega").val();
    var dataFinal = $("#data-final-entrega").val();

    $.ajax({
        url: '/SolicitacaoColeta/AtualizarListaSolicitacoes',
        type: 'GET',
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        beforeSend: function () {
            $("#atualizar-lista-coletas").find('i').removeAttr('class').addClass("fa fa-sync-alt fa-spin");
            $("#atualizar-lista-coletas").attr('disabled', 'disabled');
        },
        data: { dataInicial: dataInicial, dataFinal: dataFinal, pagina: pagina },
        success: function (retorno) {
            var painelGeral = $("#painel-geral");

            painelGeral.children().remove();

            $.each(retorno.Lista, function (index, data) {
                var painelSolicitacao = $('<div class="panel panel-body bg-light border painel-solicitacao" style="margin-top:15px;">' +
                    '    <div class="form-horizontal">' +
                    '        <div class="form-group">' +
                    '            <div class="row">' +
                    '                <input type="hidden" class="id-solicitacao" value="' + data.Id + '"/>' +
                    '                <div class="col-md-2">' +
                    '                    <label class="control-label col-md-10" style="font-weight:bold;">Nº Controle:</label>' +
                    '                    <div class="col-md-10">' +
                    '                        <label>' + data.CodigoControle + '</label>' +
                    '                    </div>' +
                    '                </div>' +
                    '                <div class="col-md-5">' +
                    '                    <label class="control-label col-md-7" style="font-weight:bold;">Cidade de destino:</label>' +
                    '                    <div class="col-md-10">' +
                    '                        <label>' + data.CidadeDestino + '</label>' +
                    '                    </div>' +
                    '                </div>' +
                    '                <div class="col-md-1">' +
                    '                    <label class="control-label col-md-10" style="font-weight:bold;">Estado:</label>' +
                    '                    <div class="col-md-10">' +
                    '                        <label>' + data.EstadoDestino + '</label>' +
                    '                    </div>' +
                    '                </div>' +
                    '                <div class="col-md-3">' +
                    '                    <label class="control-label col-md-12" style="font-weight:bold;">Data Máxima de Entrega:</label>' +
                    '                    <div class="col-md-10">' +
                    '                        <label>' + data.DataEntregaTexto + '</label>' +
                    '                    </div>' +
                    '                </div>' +
                    '            </div>' +
                    '        </div>' +
                    '        <div class="form-group">' +
                    '            <div class="col-md-12">' +
                    '                <button type="button" class="btn btn-primary col-md-12" data-toggle="collapse" href="#multiCollapseExample' + index + '" role="button" aria-expanded="false" aria-controls="multiCollapseExample1"><i class="fa fa-cube exibir-produtos-coleta"> Detalhes</i></button>' +
                    '            </div>' +
                    '        </div>' +
                    '        <div class="form-group">' +
                    '            <div class="col">' +
                    '                <div class="collapse multi-collapse" id="multiCollapseExample' + index + '">' +
                    '                    <div class="card card-body">' +
                    '                        <div class="row">' +
                    '                            <div class="col-md-7">' +
                    '                                <label class="control-label col-md-2" style="font-weight:bold;">Cliente:</label>' +
                    '                                <div class="col-md-10">' +
                    '                                    <label>' + data.Usuario.NomeUsuario + '</label>' +
                    '                                </div>' +
                    '                            </div>' +
                    '                            <div class="col-md-5">' +
                    '                                <label class="control-label col-md-12" style="font-weight:bold;">Status:</label>' +
                    '                                <div class="col-md-10">' +
                    '                                    <label>' + RetornarTextoStatus(data.Status) + '</label>' +
                    '                                </div>' +
                    '                            </div>' +
                    '                        </div>' +
                    '                        <div class="row">' +
                    '                            <div class="col-md-7">' +
                    '                                <label class="control-label col-md-5" style="font-weight:bold;">Veículo:</label>' +
                    '                                <div class="col-md-10">' +
                    '                                    <select class="col-md-12 selecao-veiculo"></select>' +
                    '                                </div>' +
                    '                            </div>' +
                    '                            <div class="col-md-5">' +
                    '                                <label class="control-label col-md-12" style="font-weight:bold;">Motorista:</label>' +
                    '                                <div class="col-md-10">' +
                    '                                    <select class="col-md-12 selecao-motorista"></select>' +
                    '                                </div>' +
                    '                            </div>' +
                    '                        </div>' +
                    '                        <div class="form-group" style="margin-top:10px;">' +
                    '                            <div class="col-md-12">' +
                    '                                <button type="button" class="btn btn-primary col-md-12 solicitar-coleta"><i class="fa fa-cube exibir-produtos-coleta"> Solicitar Coleta</i></button>' +
                    '                            </div>' +
                    '                        </div>' +
                    '                    </div>' +
                    '                </div>' +
                    '            </div>' +
                    '        </div>' +
                    '    </div>' +
                    '</div>');

                painelGeral.append(painelSolicitacao);
            });

            var objPainelPaginacao = $("#painel-paginacao");
            objPainelPaginacao.find("#paginacao").remove();

            var textoPaginacao = '<nav aria-label="Page navigation" id="paginacao" style="margin-top:10px;">' +
                '    <ul class="pagination">';

            for (var cont = 0; cont < retorno.TotalPaginas; cont++) {
                var textoActive = "";

                if (pagina == (cont + 1)) {
                    textoActive = "active";
                }

                textoPaginacao = textoPaginacao + '        <li class="page-item ' + textoActive + '"><a class="page-link botao-paginacao" href="#" data-numero-paginacao="' + (cont + 1) + '">' + (cont + 1) + '</a></li>';
            }

            textoPaginacao = textoPaginacao + '    </ul>' +
                '</nav>'

            var objControlePaginacao = $(textoPaginacao);
            objPainelPaginacao.append(objControlePaginacao);

            $(".botao-paginacao").on("click", function (e) {
                e.preventDefault();
                AtualizarListaSolicitacoes($(this).attr("data-numero-paginacao"));
            });

            $('.selecao-veiculo').select2({
                theme: 'bootstrap'
            });

            var listaVeiculos = [];

            $.ajax({
                url: '/SolicitacaoColeta/RetornarVeiculosDisponiveis',
                type: 'GET',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    var resultados = [];

                    listaVeiculos.push({ id: "", text: "" });

                    $.each(data, function (index, item) {
                        listaVeiculos.push({
                            id: item.Placa,
                            text: item.Placa + " - " + item.Modelo
                        });
                    });

                    $('.selecao-veiculo').select2({
                        data: listaVeiculos,
                        theme: 'bootstrap',
                        placeholder: "Selecione um veículo"
                    });
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.error(xhr);
                    console.error(ajaxOptions);
                    console.error(thrownError);
                }
            });

            $('.selecao-motorista').select2({
                theme: 'bootstrap'
            });

            var listaMotoristas = [];

            $.ajax({
                url: '/SolicitacaoColeta/RetornarMotoristasDisponiveis',
                type: 'GET',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    var resultados = [];

                    listaMotoristas.push({ id: "", text: "" });

                    $.each(data, function (index, item) {
                        listaMotoristas.push({
                            id: item.RegistroFuncionario,
                            text: item.RegistroFuncionario + " - " + item.Nome
                        });
                    });

                    $('.selecao-motorista').select2({
                        data: listaMotoristas,
                        theme: 'bootstrap',
                        placeholder: "Selecione um motorista"
                    });
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.error(xhr);
                    console.error(ajaxOptions);
                    console.error(thrownError);
                }
            });

            $(".solicitar-coleta").on("click", function () {
                SolicitarColeta($(this));
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error(xhr);
            console.error(ajaxOptions);
            console.error(thrownError);
        }
    }).always(function () {
        $("#atualizar-lista-coletas").find('i').removeAttr('class').addClass("fa fa-sync-alt");
        $("#atualizar-lista-coletas").removeAttr('disabled');
    });
}

function SolicitarColeta(botaoEvento) {
    var painelSolicitacoes = botaoEvento.parents(".painel-solicitacao");

    var idSolicitacao = painelSolicitacoes.find(".id-solicitacao");
    var selecaoVeiculo = painelSolicitacoes.find(".selecao-veiculo");
    var selecaoMotorista = painelSolicitacoes.find(".selecao-motorista");

    var objColeta = { idSolicitacao: idSolicitacao.val(), placaVeiculo: selecaoVeiculo.val(), registroMotorista: selecaoMotorista.val() };

    $.ajax({
        url: '/SolicitacaoColeta/SolicitarColeta',
        type: 'GET',
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: objColeta,
        success: function (data) {
            if (data.Erro) {
                toastr.error(data.Mensagem);
            } else {
                toastr.info(data.Mensagem);
                AtualizarListaSolicitacoes(1);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.error(xhr);
            console.error(ajaxOptions);
            console.error(thrownError);
        }
    });
}

$(document).ready(function () {
    $("#atualizar-lista-coletas").on("click", function () {
        AtualizarListaSolicitacoes(1);
    });

    AtualizarListaSolicitacoes(1);

    toastr.options = {
        positionClass: 'toast-top-full-width',
        timeOut: 4000
    }
});