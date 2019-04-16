function ReordenarListaProdutos() {
    var listaProdutos = $(".painel-produto-adicionado");

    $.each(listaProdutos, function (index, data) {
        var objData = $(data);

        var objDescricao = objData.find(".descricao-produto-lista");
        objDescricao.attr("name", 'Produtos[' + index + '].DescricaoProduto');

        var objQuantidade = objData.find(".quantidade-produto");
        objQuantidade.attr("name", 'Produtos[' + index + '].Quantidade');

        var objPeso = objData.find(".peso-produto");
        objPeso.attr("name", 'Produtos[' + index + '].Peso');

        var objAltura = objData.find(".altura-produto");
        objAltura.attr("name", 'Produtos[' + index + '].Altura');

        var objLargura = objData.find(".largura-produto");
        objLargura.attr("name", 'Produtos[' + index + '].Largura');

        var objComprimento = objData.find(".comprimento-produto");
        objComprimento.attr("name", 'Produtos[' + index + '].Comprimento');
    });
}

$(document).ready(function () {
    $(".remove-produto").on("click", function () {
        var objPai = $(this).parents(".painel-produto-adicionado").remove();
    });

    $("#adicionar-produto").on("click", function () {
        var objListaProdutos = $("#lista-produtos");

        var indice = $(".painel-produto-adicionado").length;

        var objProduto = $('<div class="panel panel- body bg-light border painel-produto-adicionado" style="margin-top:10px;">' +
            '    <div class="form-group">' +
            '        <div class="row" style="margin-top:10px;">' +
            '            <div class="col-md-12">' +
            '                 <label class="control-label col-md-3">Descrição do Produto</label>' +
            '                 <div class="col-md-12">' +
            '                     <input type="text" class="form-control descricao-produto-lista" name="Produtos[' + indice + '].DescricaoProduto" />' +
            '                 </div>' +
            '            </div>' +
            '        </div>' +
            '        <div class="row" style="margin-top:10px;">' +
            '            <div class="col-md-6">' +
            '                 <label class="control-label col-md-5">Quantidade (Un.)</label>' +
            '                 <div class="col-md-12">' +
            '                     <input type="number" class="form-control quantidade-produto" name="Produtos[' + indice + '].Quantidade" />' +
            '                 </div>' +
            '            </div>' +
            '            <div class="col-md-6">' +
            '                <label class="control-label col-md-5">Peso (Kg)</label>' +
            '                <div class="col-md-12">' +
            '                    <input type="number" class="form-control peso-produto" name="Produtos[' + indice + '].Peso" />' +
            '                </div>' +
            '            </div>' +
            '        </div>' +
            '        <div class="row" style="margin-top:10px;">' +
            '            <div class="col-md-4">' +
            '                <label class="control-label col-md-7">Altura (cm)</label>' +
            '                <div class="col-md-12">' +
            '                    <input type="number" class="form-control altura-produto" name="Produtos[' + indice + '].Altura" />' +
            '                </div>' +
            '            </div>' +
            '            <div class="col-md-4">' +
            '                <label class="control-label col-md-7">Largura (cm)</label>' +
            '                <div class="col-md-12">' +
            '                    <input type="number" class="form-control largura-produto" name="Produtos[' + indice + '].Largura" />' +
            '                </div>' +
            '            </div>' +
            '            <div class="col-md-4">' +
            '                <label class="control-label col-md-7">Comprimento (cm)</label>' +
            '                <div class="col-md-12">' +
            '                    <input type="number" class="form-control comprimento-produto" name="Produtos[' + indice + '].Comprimento" />' +
            '                </div>' +
            '            </div>' +
            '        </div>' +
            '        <div class="row" style="margin-top:10px;">' +
            '            <div class="col-md-12">' +
            '                <div class="col-md-12">' +
            '                    <button class="btn btn-danger col-md-12 remove-produto"><i class="fa fa-trash"></i> Remover Produto</button>' +
            '                </div>' +
            '            </div>' +
            '        </div>' +
            '    </div>' +
            '</div >');

        objListaProdutos.append(objProduto);

        objProduto.find(".remove-produto").on("click", function () {
            var objPai = $(this).parents(".painel-produto-adicionado").remove();

            ReordenarListaProdutos();
        });
    });

    $("#simular-frete").on("click", function () {
        var listaProdutosTela = $(".painel-produto-adicionado");
        var listaProdutos = [];

        var cidadeDestino = $("#cidade-destino").val();
        var estadoDestino = $("#estado-destino").val();

        if (listaProdutosTela.length > 0) {
            $.each(listaProdutosTela, function (index, data) {
                var quantidade = $(data).find(".quantidade-produto");
                var peso = $(data).find(".peso-produto");
                var altura = $(data).find(".altura-produto");
                var largura = $(data).find(".largura-produto");
                var comprimento = $(data).find(".comprimento-produto");

                var objProduto = {
                    Quantidade: quantidade.val(),
                    Peso: peso.val(),
                    Altura: altura.val(),
                    Largura: largura.val(),
                    Comprimento: comprimento.val()
                };

                listaProdutos.push(objProduto);
            });

            var objFrete = {
                CidadeDestino: cidadeDestino,
                UfDestino: estadoDestino,
                Produtos: listaProdutos
            };

            $.ajax({
                url: '/SolicitacaoTransporte/SimularFrete',
                type: 'POST',
                dataType: "json",
                data: JSON.stringify(objFrete),
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data.Erro) {
                        alert(data.Mensagem)
                    } else {
                        $("#valor-frete").val(data.Valor);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.error(xhr);
                    console.error(ajaxOptions);
                    console.error(thrownError);
                }
            });
        }
    });

    function limpa_formulário_cep() {
        $("#rua-destino").val("");
        $("#bairro-destino").val("");
        $("#cidade-destino").val("");
        $("#estado-destino").val("");
    }

    $("#cep-destino").blur(function () {
        var cep = $(this).val().replace(/\D/g, '');

        if (cep != "") {
            var validacep = /^[0-9]{8}$/;

            if (validacep.test(cep)) {
                $("#rua-destino").val("...");
                $("#bairro-destino").val("...");
                $("#cidade-destino").val("...");
                $("#estado-destino").val("...");

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        $("#rua-destino").val(dados.logradouro);
                        $("#bairro-destino").val(dados.bairro);
                        $("#cidade-destino").val(dados.localidade);
                        $("#estado-destino").val(dados.uf);
                    } else {
                        limpa_formulário_cep();
                        alert("CEP não encontrado.");
                    }
                });
            } else {
                limpa_formulário_cep();
                alert("Formato de CEP inválido.");
            }
        }
        else {
            limpa_formulário_cep();
        }
    });
});