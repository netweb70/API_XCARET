using Microsoft.AspNetCore.Mvc;
using Models;
using Domain;

namespace API_EntriesConsume.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrieController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger<EntrieController> _logger;
        private IGenericRepositoryBase _unitOfWork;
        private string? apiUrl = string.Empty;

        public EntrieController(IGenericRepositoryBase unitOfWork, IConfiguration configuration, ILogger<EntrieController> logger)
        {
            Configuration = configuration;
            apiUrl = Configuration.GetValue<string>("URLs:Entries");
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [Route("GetNodoHTTPS/{boleano}")]
        [HttpGet]
        public async Task<List<Entrie>> GetNodoHTTPS(bool boleano)
        {
            List<Entrie> items = new List<Entrie>();

            try
            {
                items = await _unitOfWork.GetNodoHTTPS(boleano, apiUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return items;
        }

        [Route("GetCategory/")]
        [HttpGet]
        public async Task<List<string>> GetCategory()
        {
            List<string> list = new List<string>();

            try
            {
                list = await _unitOfWork.GetCategory(apiUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            return list;
        }





    }
}
