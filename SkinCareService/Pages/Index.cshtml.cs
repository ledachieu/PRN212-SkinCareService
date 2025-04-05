using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages;

public class IndexModel : PageModel
{
    private readonly BlogDAO _blogDAO;
    private readonly ServiceDAO _serviceDAO;
    private readonly TherapistDAO _therapistDAO;

    public IndexModel(BlogDAO blogDAO,ServiceDAO serviceDAO,TherapistDAO therapistDAO)
    {
        _blogDAO = blogDAO;
        _serviceDAO = serviceDAO;
        _therapistDAO = therapistDAO;
    }

    public List<Blog> LatestBlogs { get; set; }
    public List<SkinService> LatestServices { get; set; }
    public List<Therapist> LatestTherapists { get; set; }

    public void OnGet()
    {
        LatestBlogs = _blogDAO.GetLatestBlogs(3);
        LatestServices = _serviceDAO.GetLatestServices(3);
        LatestTherapists = _therapistDAO.GetLatestTherapists(3);
    }
}
