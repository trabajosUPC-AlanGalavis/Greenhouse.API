using AutoMapper;
using Greenhouse.API.Security.Domain.Models;
using Greenhouse.API.Security.Domain.Repositories;
using Greenhouse.API.Security.Domain.Services;
using Greenhouse.API.Security.Domain.Services.Communication;
using Greenhouse.API.Security.Exceptions;
using Greenhouse.API.Shared.Domain.Repositories;
using ByCryptNet = BCrypt.Net.BCrypt;

namespace Greenhouse.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        //validate
        if (_userRepository.ExistsByUsername(request.Username))
            throw new AppException($"Username {request.Username} is already taken");
        
        //map model to new user object
        var user = _mapper.Map<User>(request);
        
        //hash password
        user.PasswordHash = ByCryptNet.HashPassword(request.Password);
        
        //save user
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }
    
    //helper methods
    
    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);
        
        //validate
        if(_userRepository.ExistsByUsername(request.Username))
            throw new AppException("Username '" + request.Username + "' is already taken");
        
        //hash password if it was entered
        if (!string.IsNullOrWhiteSpace(request.Password))
            user.PasswordHash = ByCryptNet.HashPassword(request.Password);
        
        //copy model to user and save
        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);

        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
}