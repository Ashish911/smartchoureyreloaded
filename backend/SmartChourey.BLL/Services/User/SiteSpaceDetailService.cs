using BusinessLogicLayer.Configuration;
using Microsoft.AspNetCore.Http;
using SmartChourey.BLL.Services.Interfaces.User;
using SmartChourey.DAL;
using SmartChourey.DAL.Repositories.Interfaces;

namespace SmartChourey.BLL.Services.User
{
    public class SiteSpaceDetailService : ISiteSpaceDetailService
    {
        private readonly IRepository<SiteSpaceDetail> _siteSpaceDetailRepository;
        private readonly IRepository<SiteSpaceAggregate> _siteSpaceAggregateRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SiteSpaceDetailService(IRepository<SiteSpaceDetail> siteSpaceDetailRepository, IRepository<SiteSpaceAggregate> siteSpaceAggregateRepository, IHttpContextAccessor httpContextAccessor)
        {
            _siteSpaceDetailRepository = siteSpaceDetailRepository;
            _siteSpaceAggregateRepository = siteSpaceAggregateRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> DeleteByCategoryDetailId(long id, string siteId, EnumHelpers.ECategory category, EnumHelpers.EUploadType uploadType)
        {
            var isSuccess = false;
            var siteSpaceDetail = _siteSpaceDetailRepository.Table.Where(x => x.CategoryDetailId == id && x.Ecategory == (int)category && x.EuploadType == (int)uploadType).FirstOrDefault();
            var siteSpaceAggregate = _siteSpaceAggregateRepository.Table.Where(x => x.SiteId == siteId).FirstOrDefault();
            if (siteSpaceDetail != null && siteSpaceAggregate != null)
            {
                siteSpaceAggregate.UsedSpaceInMb -= siteSpaceDetail.UsedSpaceInMb;
                try
                {
                    await _siteSpaceDetailRepository.Remove(siteSpaceDetail);
                    await _siteSpaceAggregateRepository.Update(siteSpaceAggregate);
                    isSuccess = true;

                    _httpContextAccessor.HttpContext.Session.SetString("SpaceUsed", siteSpaceAggregate.UsedSpaceInMb.ToString());
                }
                catch (Exception)
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }

        public async Task<bool> DeleteByCategoryId(long id, string siteId, EnumHelpers.ECategory category)
        {
            var isSuccess = false;
            var siteSpaceDetail = _siteSpaceDetailRepository.Table.Where(x => x.CategoryId == id && x.Ecategory == (int)category && x.SiteId == siteId).ToList();
            var siteSpaceAggregate = _siteSpaceAggregateRepository.Table.Where(x => x.SiteId == siteId).FirstOrDefault();
            float spaceToBeRemoved = 0.0f;
            if (siteSpaceDetail != null && siteSpaceAggregate != null)
            {
                foreach (var item in siteSpaceDetail)
                {
                    spaceToBeRemoved += item.UsedSpaceInMb;
                    await _siteSpaceDetailRepository.Remove(item);
                }

                siteSpaceAggregate.UsedSpaceInMb -= spaceToBeRemoved;
                try
                {
                    await _siteSpaceAggregateRepository.Update(siteSpaceAggregate);
                    isSuccess = true;

                    _httpContextAccessor.HttpContext.Session.SetString("SpaceUsed", siteSpaceAggregate.UsedSpaceInMb.ToString());
                }
                catch (Exception)
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }

        public async Task<bool> DeleteBySiteSpaceDetailId(long id, string siteId)
        {
            var isSuccess = false;
            var siteSpaceDetail = _siteSpaceDetailRepository.Table.Where(x => x.SiteSpaceDetailId == id).FirstOrDefault();
            var siteSpaceAggregate = _siteSpaceAggregateRepository.Table.Where(x => x.SiteId == siteId).FirstOrDefault();
            if (siteSpaceDetail != null && siteSpaceAggregate != null)
            {
                siteSpaceAggregate.UsedSpaceInMb -= siteSpaceDetail.UsedSpaceInMb;
                try
                {
                    await _siteSpaceDetailRepository.Remove(siteSpaceDetail);
                    await _siteSpaceAggregateRepository.Update(siteSpaceAggregate);
                    isSuccess = true;

                    _httpContextAccessor.HttpContext.Session.SetString("SpaceUsed", siteSpaceAggregate.UsedSpaceInMb.ToString());
                }
                catch (Exception)
                {
                    isSuccess = false;
                }
            }
            return isSuccess;
        }

        public async Task<bool> Insert(SiteSpaceDetail data)
        {
            var isSuccess = false;
            try
            {
                await _siteSpaceDetailRepository.Add(data);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
