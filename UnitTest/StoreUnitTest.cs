using Domain.Repositories;
using Services;
using AutoMapper;
using API.Controllers;
using Shared.Dtos;
using Moq;
using Xunit;
using Services.Mappers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;
using System.Reflection;

namespace UnitTest
{
    public class StoreUnitTest
    {
        private IMapper _mapper;
        private Mock<IRepositoryManager> _repositoryManagerMock;
        private StoreController _controller;

        public StoreUnitTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());

            _mapper = configuration.CreateMapper();

            _repositoryManagerMock = new Mock<IRepositoryManager>();

            ServiceManager serviceManager = new ServiceManager(_repositoryManagerMock.Object, _mapper);

            _controller = new StoreController(serviceManager);
        }

        [Fact]
        public async Task Test_Store_Register_Must_Work()
         {
            StoreForCreateDto requestStore = new StoreForCreateDto() {Name = "X eletrodométicos", Address = "Rua x nº x^2, x^3 - x^4"};

            Store store = _mapper.Map<Store>(requestStore);

            StoreDto expectedStore = _mapper.Map<StoreDto>(store);

            _repositoryManagerMock.Setup(x => x.StoreRepository.AddStore(It.IsAny<Store>())).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();

            var result = await _controller.Add(requestStore);

            Assert.NotNull(result);
            _repositoryManagerMock.VerifyAll();
        }

        [Fact]
        public async Task Test_Store_Update_Must_Work()
        {
            Store store = new Store() {Name = "Loja x", Address = "Rua x nº x^2, x^3 - x^4"};
            StoreForUpdateDto requestStore = new StoreForUpdateDto() {Name = "Loja x", Address = "Rua x nº x^2, x^3 - x^4"};
            
             _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync(store);
             _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();
            _repositoryManagerMock.Setup(x => x.StoreRepository.UpdateStore(It.IsAny<Store>())).Verifiable();

            var result = await _controller.UpdateById(Guid.Empty, requestStore);

            Assert.True(result is OkObjectResult);

            _repositoryManagerMock.VerifyAll();

            Assert.NotNull(result);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);

            StoreDto updatedStore = (StoreDto)response.Value;
            StoreDto expectedStore = _mapper.Map<StoreDto>(store);

            Assert.Equal(updatedStore.ToString(), expectedStore.ToString());

            Assert.Equal(response.StatusCode, 200);
        }

         [Fact]
        public async Task Test_Store_Update_Must_Throw_NotFoundException()
        {
            StoreForUpdateDto store = new StoreForUpdateDto() {Name = "Loja x", Address = "Rua x nº x^2, x^3 - x^4"};
            
             _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync((Store?)null);

             try
             {
                await _controller.UpdateById(Guid.Empty, store);
                Assert.Fail();
             }
             catch(Exception ex)
             {
                Assert.True(ex is StoreNotFoundException);
             }
        }

        [Fact]
        public async Task Test_Store_Delete_Must_Work()
        {
            Store store = new Store() {Name = "Loja x", Address = "Rua x nº x^2, x^3 - x^4"};

             _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync(store);
             _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();
            _repositoryManagerMock.Setup(x => x.StoreRepository.DeleteStore(It.IsAny<Store>())).Verifiable();

             var result = await _controller.Delete(Guid.Empty);

             _repositoryManagerMock.VerifyAll();
            
            Assert.NotNull(result);
            Assert.True(result is NoContentResult);
            
            NoContentResult response = result as NoContentResult;

            Assert.NotNull(response);
            Assert.Equal(response.StatusCode, 204);
        }

        [Fact]
        public async Task Test_Store_Delete_Must_Throw_NotFoundException()
        {
             _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync((Store?)null);

            try
            {
                await _controller.Delete(Guid.Empty);
                Assert.Fail();
            }
            catch(Exception ex) 
            {
                Assert.True(ex is StoreNotFoundException);
            }   
        }

        [Fact]
        public async Task Test_Store_Get_Must_Work()
        {
            Store store = new Store() {Name = "Loja x", Address = "Rua x nº x^2, x^3 - x^4"};

             _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync(store);

             var result = await _controller.GetById(Guid.Empty);

             _repositoryManagerMock.VerifyAll();

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);

            StoreDto resultStore = (StoreDto)response.Value;
            StoreDto expectedStore = _mapper.Map<StoreDto>(store);

            Assert.Equal(resultStore.ToString(), expectedStore.ToString());

            Assert.Equal(response.StatusCode, 200);
        }

        [Fact]
        public async Task Test_Store_Get_Must_Return_Null()
        {
             _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync((Store?)null);

             var result = await _controller.GetById(Guid.Empty);

             Assert.NotNull(result);
             Assert.True(result is NotFoundResult);

             NotFoundResult response = result as NotFoundResult;

             Assert.Equal(response.StatusCode, 404);
        }

        [Fact]
        public async Task Test_Store_GetAll_Must_Work()
        {
            List<Store> stores = new List<Store>() {
                new Store() {Name = "Loja x", Address = "Rua x nº x^2, x^3 - x^4"},
                new Store() {Name = "Loja y", Address = "Rua y nº y^2, y^3 - y^4"},
                new Store() {Name = "Loja z", Address = "Rua z nº z^2, z^3 - z^4"}
            };

            _repositoryManagerMock.Setup(x => x.StoreRepository.GetAllStoresAsync()).ReturnsAsync(stores);

            var result = await _controller.IndexAll();

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);

            List<StoreDto> allStores = (List<StoreDto>)response.Value;

            Assert.Equal(stores.Count(), allStores.Count());

            for(int i = 0; i < stores.Count(); ++i)
                Assert.Equal(_mapper.Map<StoreDto>(stores[i]).ToString(), allStores[i].ToString());

            Assert.Equal(response.StatusCode, 200);
        }
    }
}