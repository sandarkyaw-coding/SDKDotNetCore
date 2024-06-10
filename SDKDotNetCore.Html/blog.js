const tblBlog = "blogs";
let blogId = null;
getBlogsTable();
//readBlog();
//createBlog("one", "two", "three");
//updateBlog("67d79e72-acc2-42d3-92bd-e06bcb8bb6a7", "hello", "one", "two");
//deleteBlog("71254131-7fb9-453d-8c2c-da8b76a11028");

function readBlog(){
    let lst = getBlogs();
    console.log(lst);
}

function editBlog(id){
    let lst = getBlogs();

    const items = lst.filter(x=> x.id === id);
    console.log(items);

    if(items.length == 0){
        console.log("No Data Found.");
        errorMessage("There is no data.");
        return;
    }

    let item = items[0];

    blogId = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();

}

function createBlog(title, author, content){
    let lst = getBlogs();

    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(requestModel);
    
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Saving Successful.");
}

function updateBlog(id, title, author, content){
    let lst = getBlogs();

    const items = lst.filter(x=> x.id === id);
    console.log(items);

    if(items.length == 0){
        console.log("No Data Found.");
        errorMessage("There is no data.");
        return;
    }

    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Updating Successful.");
}

function deleteBlog(id){
    let result = confirm("Are you sure want to delete?");
    if(!result) return;

    let lst = getBlogs();

    const items = lst.filter(x=> x.id === id);

    if(items.length == 0){
        console.log("No Data Found.");
        return;
    }

    lst = lst.filter(x => x.id !== id);
    
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    const blog = localStorage.getItem(tblBlog);
    console.log(blog)

    successMessage("Deleting Successful.");

    getBlogsTable();
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
      (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
  }

function getBlogs(){
    const blogs = localStorage.getItem(tblBlog);

    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }

    return lst;
}

$('#btnSave').click(function(){
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if(blogId === null){
        createBlog(title, author, content);
    }
    else{
        updateBlog(blogId, title, author, content);
        blogId = null;
    }
    clearControls();
    getBlogsTable();
})

function successMessage(message){
    alert(message);
}

function errorMessage(message){
    alert(message);
}

function clearControls(){
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogsTable(){
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
    <tr>
      <td>
      <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
      <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
      </td>
      <td>${++count}</td>
      <td>${item.title}</td>
      <td>${item.author}</td>
      <td>${item.content}</td>
    </tr>
        `;
        htmlRows += htmlRow;
    });
    $('#tbody').html(htmlRows);
}