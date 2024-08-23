$(document).ready(function() {

	var SPMaskBehavior = function (val) {
      return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
    spOptions = {
      onKeyPress: function(val, e, field, options) {
          field.mask(SPMaskBehavior.apply({}, arguments), options);
        }
    };
	
	//$("input.mask-cpf").mask("999.999.999-99");
    $('input.money').mask('#.##0,00', { reverse: true });
    $('input.parcel').mask('#0', { reverse: true });
	$("input.mask-telefone").mask(SPMaskBehavior, spOptions);
    $("input.mask-cep").mask("00.000-000");
    $("input.mask-data").mask("00r00r0000", {
        translation: {
            'r': {
                pattern: /[\/]/,
                fallback: '/'
            },
            placeholder: "__/__/____"
        }
    });
    var options = {
        onKeyPress: function (cpf, ev, el, op) {
            var masks = ['000.000.000-000', '00.000.000/0000-00'];
            $('input.mask-cpf').mask((cpf.length > 14) ? masks[1] : masks[0], op);
        }
    }
    $('input.mask-cpf').length > 11 ? $('input.mask-cpf').mask('00.000.000/0000-00', options) : $('input.mask-cpf').mask('000.000.000-00#', options);
    $('input.entrada').mask('#.##0,00', { reverse: true });
    //Funcoes para navegacao # nossa historia
    $(document).on('click', '.timeline li h2', function (e) {
        e.preventDefault();

		if($(this).hasClass('active')) {

		} else {
			var $pergunta = $(this);
			$pergunta.parents('ul.timeline').find('h2').removeClass('active');
			$pergunta.addClass('active');
			$pergunta.parents('ul.timeline').find('p').stop(true, true).slideUp(400);
			$pergunta.parents('ul.timeline li').find('p').stop(true, true).delay(400).slideDown(400);
		}

		return false;
    }); 

    $('#consulta-cpf-finalizar .links .linha-digitavel').click(function (e) {
        var value = $(this).attr("data-value");
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(value).select();
        document.execCommand("copy");
        $temp.remove();
        $(this).css("background-color", "#E6FFEC").css("border-color", "#7DD077"); 
        e.preventDefault();
    });

    $('#consulta-cpf-finalizar .links a.baixar-pdf').click(function (e) {
        $(this).css("background-color", "#E6FFEC").css("border-color", "#7DD077");
    });

	var windowWidth = $(window).width();
	if (windowWidth <= 640) {
		$(document).on('click', '.central-de-cobranca ul li', function (e) {
			e.preventDefault();

			$('.central-de-cobranca ul li').removeClass('active');
			$(this).addClass('active');

			return false;
		});
    }


    $('.btn-negociar-agora, .btn-consultar-cpf, button.btn-nova-consulta, .lista-telefone .btn-voltar, .lista-telefone .btn-continuar, #pg-consulta-cpf-forma-de-pagamento .form form .btn-enviar, #consulta-cpf-termos-e-condicoes form .btn-concordo, #consulta-cpf-termos-e-condicoes .btn-voltar').click(function () {
        $('body').css("overflow", "hidden");
        $('#load-page, #load-page .background-opacity').css({
            width: window.innerWidth,
            height: screen.height
        });
        $('#load-page').fadeIn(300);
    });

});

$(window).bind('scroll', function (ev) {
    var scrollY = $(this).scrollTop();
    $('#load-page').css("top", scrollY);
    $('#load-page, #load-page .background-opacity').css({
        width: window.innerWidth,
        height: screen.height
    });
});

 $(window).on('load', function () {

	var windowWidth = $(window).width();

    if (windowWidth <= 640) {

		setTimeout(function() {

		 /*  $('html,body').stop().animate({
				scrollTop: ($('.scroll-secao').offset().top)
			}, 700);
*/
		},500);

    }

});