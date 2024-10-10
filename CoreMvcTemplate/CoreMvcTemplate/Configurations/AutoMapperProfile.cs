using AutoMapper;
using CoreMvcTemplate.Entities;
using CoreMvcTemplate.Models.Blog;
using CoreMvcTemplate.Models.LandingPages;
using CoreMvcTemplate.Models.MarketPlaceModels;
using CoreMvcTemplate.Models.PdfFile;
using CoreMvcTemplate.Models.Products;
using CoreMvcTemplate.Models.Sliders;

namespace CoreMvcTemplate.Configurations
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Blog,EditBlogViewModel>().ReverseMap();
            CreateMap<Blog,AddBlogViewModel>().ReverseMap();
            CreateMap<Blog,DetailBlogViewModel>().ReverseMap();
            CreateMap<Blog,BlogPropViewModel>().ReverseMap();

            CreateMap<MarketPlace,AddMarketPlaceViewModel>().ReverseMap();
            CreateMap<MarketPlace,EditMarketPlaceViewModel>().ReverseMap();

            CreateMap<Product,AddProductViewModel>().ReverseMap();
            CreateMap<Product,EditProductViewModel>().ReverseMap();
            CreateMap<Product, ProductPropViewModel>().ReverseMap();

            CreateMap<Slider, AddSliderViewModel>().ReverseMap();
            CreateMap<Slider, EditSliderViewModel>().ReverseMap();

            CreateMap<EditLandingPageViewModel, LandingPage>().ReverseMap();


            CreateMap<AddPdfFileViewModel, PdfFile>().ReverseMap();



        }
    }
}
