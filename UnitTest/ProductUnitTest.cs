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
    public class ProductUnitTest
    {
        private IMapper _mapper;
        private Mock<IRepositoryManager> _repositoryManagerMock;
        private ProductController _controller;

        public ProductUnitTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>());

            _mapper = configuration.CreateMapper();

            _repositoryManagerMock = new Mock<IRepositoryManager>();

            ServiceManager serviceManager = new ServiceManager(_repositoryManagerMock.Object, _mapper);

            _controller = new ProductController(serviceManager);
        }

        [Fact]
        public async Task Test_Product_Register_Must_Work()
        {
            ProductForCreateDto requestProduct = new ProductForCreateDto() {Name = "x", Price = 1};

            Product product = _mapper.Map<Product>(requestProduct);

            ProductDto expectedProduct = _mapper.Map<ProductDto>(product);

            _repositoryManagerMock.Setup(x => x.ProductRepository.AddProduct(It.IsAny<Product>())).Verifiable();
            _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();

            var result = await _controller.Add(requestProduct);

            Assert.NotNull(result);
            _repositoryManagerMock.VerifyAll();
        }

        [Fact]
        public async Task Test_Product_Update_Must_Work()
        {
            Product product = new Product() {Name = "x", Price = 1};
            ProductForUpdateDto requestProduct = new ProductForUpdateDto() {Name = "x", Price = 1};
            
             _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync(product);
             _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();
            _repositoryManagerMock.Setup(x => x.ProductRepository.UpdateProduct(It.IsAny<Product>())).Verifiable();

            var result = await _controller.Update(Guid.Empty, requestProduct);

            Assert.True(result is OkObjectResult);

            _repositoryManagerMock.VerifyAll();

            Assert.NotNull(result);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);

            ProductDto updatedProduct = (ProductDto)response.Value;
            ProductDto expectedProduct = _mapper.Map<ProductDto>(product);

            Assert.Equal(updatedProduct.ToString(), expectedProduct.ToString());

            Assert.Equal(response.StatusCode, 200);
        }

        [Fact]
        public async Task Test_Product_Update_Must_Throw_NotFoundException()
        {
            ProductForUpdateDto product = new ProductForUpdateDto() {Name = "x", Price = 1};
            
             _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync((Product?)null);

             try
             {
                await _controller.Update(Guid.Empty, product);
                Assert.Fail();
             }
             catch(Exception ex)
             {
                Assert.True(ex is ProductNotFoundException);
             }
        }

        [Fact]
        public async Task Test_Product_Delete_Must_Work()
        {
            Product product = new Product() {Name = "x", Price = 1};

             _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync(product);
             _repositoryManagerMock.Setup(x => x.SaveAsync()).Verifiable();
            _repositoryManagerMock.Setup(x => x.ProductRepository.DeleteProduct(It.IsAny<Product>())).Verifiable();

             var result = await _controller.DeleteById(Guid.Empty);

             _repositoryManagerMock.VerifyAll();
            
            Assert.NotNull(result);
            Assert.True(result is NoContentResult);
            
            NoContentResult response = result as NoContentResult;

            Assert.NotNull(response);
            Assert.Equal(response.StatusCode, 204);
        }

        [Fact]
        public async Task Test_Product_Delete_Must_Throw_NotFoundException()
        {
             _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync((Product?)null);

            try
            {
                await _controller.DeleteById(Guid.Empty);
                Assert.Fail();
            }
            catch(Exception ex) 
            {
                Assert.True(ex is ProductNotFoundException);
            }   
        }

        [Fact]
        public async Task Test_Product_Get_Must_Work()
        {
            Product product = new Product() {Name = "x", Price = 1};

             _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync(product);

             var result = await _controller.GetById(Guid.Empty);

             _repositoryManagerMock.VerifyAll();

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);

            ProductDto resultProduct = (ProductDto)response.Value;
            ProductDto expectedProduct = _mapper.Map<ProductDto>(product);

            Assert.Equal(resultProduct.ToString(), expectedProduct.ToString());

            Assert.Equal(response.StatusCode, 200);
        }

        [Fact]
        public async Task Test_Product_Get_Must_Return_Null()
        {
             _repositoryManagerMock.Setup(x => x.ProductRepository.GetProductByIdAsync(Guid.Empty)).ReturnsAsync((Product?)null);

             var result = await _controller.GetById(Guid.Empty);

             Assert.NotNull(result);
             Assert.True(result is NotFoundResult);

             NotFoundResult response = result as NotFoundResult;

             Assert.Equal(response.StatusCode, 404);
        }

        [Fact]
        public async Task Test_Product_GetAll_Must_Work()
        {
            List<Product> products = new List<Product>() {
                new Product() {Name = "a", Price = 1},
                new Product() {Name = "b", Price = 2},
                new Product() {Name = "c", Price = 3}
            };

            _repositoryManagerMock.Setup(x => x.ProductRepository.GetAllProductsAsync()).ReturnsAsync(products);

            var result = await _controller.IndexAll();

            Assert.NotNull(result);
            Assert.True(result is OkObjectResult);

            OkObjectResult response = result as OkObjectResult;

            Assert.NotNull(response);

            List<ProductDto> allProducts = (List<ProductDto>)response.Value;

            Assert.Equal(products.Count(), allProducts.Count());

            for(int i = 0; i < products.Count(); ++i)
                Assert.Equal(_mapper.Map<ProductDto>(products[i]).ToString(), allProducts[i].ToString());

            Assert.Equal(response.StatusCode, 200);
        }
    }
}
