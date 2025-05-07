using AutoMapper;
namespace Project.DTO { 
  public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Writer, WriterDTO>();
            CreateMap<WriterDTO, Writer>();
            CreateMap<Article, ArticleDTO>();
            CreateMap<ArticleDTO, Article>();
            CreateMap<Comment, CommentDTO>();
            CreateMap<CommentDTO, Comment>();
        }
    }
}
