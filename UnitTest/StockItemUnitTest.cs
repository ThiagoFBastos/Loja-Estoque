using Domain.Repositories;
using API.Controllers;
using AutoMapper;
using Moq;
using Services;
using Services.Mappers;
using Shared.Dtos;
using Domain.Entities;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;

namespace UnitTest
{
    public class StockItemUnitTest
    { 
        private IMapper _mapper;
        private Mock<IRepositoryManager> _repositoryManagerMock;
        private StockItemController _controller;
        public StockItemUnitTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());

            _mapper = configuration.CreateMapper();

            _repositoryManagerMock = new Mock<IRepositoryManager>();

            ServiceManager serviceManager = new ServiceManager(_repositoryManagerMock.Object, _mapper);

            _controller = new StockItemController(serviceManager);
        }

        [Fact]
        public async Task Test_StockItem_Register_Must_Work()
        {
            StockItemForCreateDto requestStockItem = new StockItemForCreateDto() {Quantity = 1};

            _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync(new Store() {Name = "X", Address = "X"});
            _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync(new Product() {Name = "X", Price = 1});
            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemFromStoreAsync(Guid.Empty, Guid.Empty)).ReturnsAsync((StockItem?)null);
            _repositoryManagerMock.Setup(x => x.StockItemRepository.AddStockItem(It.IsAny<StockItem>())).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();

            var result = await _controller.Add(requestStockItem);

            Assert.NotNull(result);
            _repositoryManagerMock.VerifyAll();
        }

        [Fact]
        public async Task Test_StockItem_Register_Must_Throws_BadRequestException_StoreNotFound()
        {
            StockItemForCreateDto requestStockItem = new StockItemForCreateDto() {Quantity = 1};

            _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync((Store?)null);

            try
            {
                await _controller.Add(requestStockItem);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.True(ex is BadRequestException);
            }
        }

        [Fact]
        public async Task Test_StockItem_Register_Must_Throws_BadRequestException_ProductNotFound()
        {
            StockItemForCreateDto requestStockItem = new StockItemForCreateDto() {Quantity = 1};

            _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync(new Store() {Name = "X", Address = "X"});
            _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync((Product?)null);

            try
            {
                await _controller.Add(requestStockItem);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.True(ex is BadRequestException);
            }
        }

        [Fact]
        public async Task Test_StockItem_Register_Must_Throws_BadRequestException_StoreAndProductNotFound()
        {
            StockItemForCreateDto requestStockItem = new StockItemForCreateDto() {Quantity = 1};
            StockItem expectedStockItem = _mapper.Map<StockItem>(requestStockItem);

            _repositoryManagerMock.Setup(x => x.StoreRepository.GetStoreByIdAsync(Guid.Empty)).ReturnsAsync(new Store() {Name = "X", Address = "X"});
            _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync(new Product() {Name = "x", Price = 1});
            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemFromStoreAsync(Guid.Empty, Guid.Empty)).ReturnsAsync(new StockItem() {Quantity = 1});

            try
            {
                await _controller.Add(requestStockItem);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.True(ex is BadRequestException);
            }
        }

        [Fact]
        public async Task Test_StockItem_Delete_Must_Work()
        {
            StockItem stockItem = new StockItem() {Quantity = 1};

            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync(stockItem);
            _repositoryManagerMock.Setup(x => x.StockItemRepository.DeleteStockItem(It.IsAny<StockItem>())).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();

            var result = await _controller.DeleteById(Guid.Empty);

            _repositoryManagerMock.VerifyAll();

            Assert.NotNull(result);
            Assert.True(result is NoContentResult);

            NoContentResult response = result as NoContentResult;
            
            Assert.NotNull(response);
            Assert.Equal(response.StatusCode, 204);
        }

        [Fact]
        public async Task Test_StockItem_Delete_Must_Throw_Exception()
        {
            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync((StockItem?)null);

            try
            {
                await _controller.DeleteById(Guid.Empty);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.True(ex is StockItemNotFoundException);
            }
        }

        [Fact]
        public async Task Test_StockItem_Additems_Must_Work()
        {
            StockItemForUpdateDto requestStockItem = new StockItemForUpdateDto() {Quantity = 7};
            StockItem stockItem = new StockItem() {Quantity = 6};

            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync(stockItem);
            _repositoryManagerMock.Setup(x => x.StockItemRepository.UpdateStockItem(It.IsAny<StockItem>())).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();

            var result = await _controller.AddItems(Guid.Empty, requestStockItem);

            _repositoryManagerMock.VerifyAll();
            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);
            Assert.Equal(response.StatusCode, 200);

            StockItemDto responseStockItem = (StockItemDto)response.Value;
            StockItemDto expectedStockItem = _mapper.Map<StockItemDto>(stockItem);

            stockItem.Quantity += requestStockItem.Quantity;

            Assert.Equal(responseStockItem.ToString(), expectedStockItem.ToString());
        }

        [Fact]
        public async Task Test_StockItem_Additems_Must_Throw_Exception_StockNotFound()
        {
            StockItemForUpdateDto requestStockItem = new StockItemForUpdateDto() {Quantity = 7};

            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync((StockItem?)null);

            try
            {
                await _controller.AddItems(Guid.Empty, requestStockItem);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.True(ex is StockItemNotFoundException);
            }
        }

        [Fact]
        public async Task Test_StockItem_Removeitems_Must_Work()
        {
            StockItemForUpdateDto requestStockItem = new StockItemForUpdateDto() {Quantity = 3};
            StockItem stockItem = new StockItem() {Quantity = 10};

            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync(stockItem);
            _repositoryManagerMock.Setup(x => x.StockItemRepository.UpdateStockItem(It.IsAny<StockItem>())).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();

            var result = await _controller.RemoveItems(Guid.Empty, requestStockItem);

            _repositoryManagerMock.VerifyAll();
            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);
            Assert.Equal(response.StatusCode, 200);

            StockItemDto responseStockItem = (StockItemDto)response.Value;
            StockItemDto expectedStockItem = _mapper.Map<StockItemDto>(stockItem);

            stockItem.Quantity -= requestStockItem.Quantity;

            Assert.Equal(responseStockItem.ToString(), expectedStockItem.ToString());
        }

        [Fact]
        public async Task Test_StockItem_Removeitems_Must_Throw_Exception_StockNotFound()
        {
            StockItemForUpdateDto requestStockItem = new StockItemForUpdateDto() {Quantity = 7};

            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync((StockItem?)null);

            try
            {
                await _controller.RemoveItems(Guid.Empty, requestStockItem);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.True(ex is StockItemNotFoundException);
            }
        }

        [Fact]
        public async Task Test_StockItem_Removeitems_Must_Throw_Exception_HighQuantity()
        {
            StockItemForUpdateDto requestStockItem = new StockItemForUpdateDto() {Quantity = 10};
            StockItem stockItem = new StockItem() {Quantity = 3};

            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync(stockItem);

            try
            {
                await _controller.RemoveItems(Guid.Empty, requestStockItem);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.True(ex is BadRequestException);
            }
        }

        [Fact]
        public async Task Test_StockItem_Get_Must_Work()
        {
            StockItem stockItem = new StockItem() {Quantity = 1};
            StockItemDto expectedStockItem = _mapper.Map<StockItemDto>(stockItem);

            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync(stockItem);

            var result = await _controller.GetById(Guid.Empty);

            Assert.NotNull(result);

            Assert.True(result is OkObjectResult);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);
            Assert.Equal(response.StatusCode, 200);

            StockItemDto responseStockItem = (StockItemDto)response.Value;

            Assert.Equal(expectedStockItem.ToString(), responseStockItem.ToString());
        }

        [Fact]
        public async Task Test_StockItem_Get_Must_Return_Null()
        {
            _repositoryManagerMock.Setup(x => x.StockItemRepository.GetStockItemByIdAsync(Guid.Empty)).ReturnsAsync((StockItem?)null);

            var result = await _controller.GetById(Guid.Empty);

            Assert.NotNull(result);

            Assert.True(result is NotFoundResult);

            NotFoundResult response = result as NotFoundResult;

            Assert.NotNull(response);
            Assert.Equal(response.StatusCode, 404);
        }
    }
}
