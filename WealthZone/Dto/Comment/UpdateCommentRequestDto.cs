﻿using System.ComponentModel.DataAnnotations;

namespace WealthZone.Dto.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required(ErrorMessage = "this field is required")]
        [MinLength(3, ErrorMessage = "must be betweem 3 and 90")]
        [MaxLength(90, ErrorMessage = "must be betweem 3 and 90")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "this field is required")]
        [MinLength(3, ErrorMessage = "must be betweem 3 and 90")]
        [MaxLength(90, ErrorMessage = "must be betweem 3 and 90")]
        public string Content { get; set; } = string.Empty;
    }
}
