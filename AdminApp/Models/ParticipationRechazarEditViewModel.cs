using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.Models
{
    public class ParticipationRechazarEditViewModel
    {
        
        public int id_par { get; set; }

        [Display(Name = "Motivo:")]
        public string motivo_par { get; set; }

        [Required(ErrorMessage = "Introduzca los motivos de rechazo, por favor.")]
        public string textootrosmotivo { get; set; }


        public List<SelectListItem> Motivo_ES_List { get; } = new List<SelectListItem>
        {
            //new SelectListItem { Value = "Nombre distinto en la factura y el formulario", Text = "Nombre distinto en la factura y el formulario" },
            //new SelectListItem { Value = "DNI/CIF  distinto en la factura y el formulario", Text = "DNI/CIF  distinto en la factura y el formulario" },
            //new SelectListItem { Value = "El taller seleccionado no coincide con la factura", Text = "El taller seleccionado no coincide con la factura" },
            //new SelectListItem { Value = "El documento adjunto no es una factura", Text = "El documento adjunto no es una factura" },
            //new SelectListItem { Value = "El ticket de compra no es válido", Text = "El ticket de compra no es válido" },
            //new SelectListItem { Value = "Factura adjunta no legible", Text = "Factura adjunta no legible" },
            //new SelectListItem { Value = "Factura incompleta", Text = "Factura incompleta" },
            new SelectListItem { Value = "Factura no indica los productos en promoción", Text = "Factura no indica los productos en promoción" },
            new SelectListItem { Value = "Factura no corresponde a un taller adherido a la promoción", Text = "Factura no corresponde a un taller adherido a la promoción" },
            new SelectListItem { Value = "Factura ya ha sido premiada", Text = "Factura ya ha sido premiada" },
            //new SelectListItem { Value = "Faltan datos del usuario en la factura", Text = "Faltan datos del usuario en la factura" },
            new SelectListItem { Value = "Fecha de factura fuera de periodo promocional", Text = "Fecha de factura fuera de periodo promocional" },
            new SelectListItem { Value = "Supera el límite de participación por email y/o dni", Text = "Supera el límite de participación por email y/o dni" },
            new SelectListItem { Value = "Neumático de la factura no incluido en la promoción", Text = "Neumático de la factura no incluido en la promoción" },
            new SelectListItem { Value = "Otros", Text = "Otros" },
        };

        public List<SelectListItem> Motivo_PT_List { get; } = new List<SelectListItem>
        {
            //new SelectListItem { Value = "Os dados da fatura e da participação não coincidem", Text = "Os dados da fatura e da participação não coincidem" },
            //new SelectListItem { Value = "NIF da fatura e da participação não coincidem", Text = "NIF da fatura e da participação não coincidem" },
            new SelectListItem { Value = "Data da fatura fora do prazo da campanha", Text = "Data da fatura fora do prazo da campanha" },
            new SelectListItem { Value = "Excede o limite email/dni", Text = "Excede o limite email/dni" },
            //new SelectListItem { Value = "Fatura sem dados do participante", Text = "Fatura sem dados do participante" },
            //new SelectListItem { Value = "Fatura ilegível", Text = "Fatura ilegível" },
            //new SelectListItem { Value = "Fatura incompleta", Text = "Fatura incompleta" },
            //new SelectListItem { Value = "Participação sem fatura", Text = "Participação sem fatura" },
            new SelectListItem { Value = "Fatura sem produtos da promoção", Text = "Fatura sem produtos da promoção" },
            new SelectListItem { Value = "Fatura de uma oficina não aderente a campanha", Text = "Fatura de uma oficina não aderente a campanha" },
            new SelectListItem { Value = "Fatura já premiada", Text = "Fatura já premiada" },
            //new SelectListItem { Value = "Excede o limite email/dni", Text = "Excede o limite email/dni" },
            //new SelectListItem { Value = "Fatura Inválida", Text = "Fatura Inválida" },
            //new SelectListItem { Value = "La oficina selecionada não corresponde à fatura", Text = "La oficina selecionada não corresponde à fatura" },
            new SelectListItem { Value = "O modelo de pneu não esta incluido na promoção", Text = "O modelo de pneu não esta incluido na promoção" },
            new SelectListItem { Value = "Otros", Text = "Otros" },
        };

        public int? validated_id_par { get; set; }
    }
}
