const tblProduct = "products";
let productId = null;
let qty = 0;
let total = 0;
getProductsTable();

function readProduct() {
    let lst = getProducts();
    console.log(lst);
}

function createProduct(name, description, price) {
    let lst = getProducts();

    const requestModel = {
        id: uuidv4(),
        name: name,
        description: description,
        price: price,
        quantity: qty,
        total : total
    };

    lst.push(requestModel);

    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(tblProduct, jsonProduct);

    successMessage("Saving Successful.");
}

function getProducts() {
    const products = localStorage.getItem(tblProduct);

    let lst = [];
    if (products !== null) {
        lst = JSON.parse(products);
    }

    return lst;
}

$("#btnSave").click(function () {
    const name = $("#name").val();
    const description = $("#description").val();
    const price = $("#price").val();

    createProduct(name, description, price);
    
    clearControls();
    getProductsTable();
});

function clearControls() {
    $("#name").val("");
    $("#description").val("");
    $("#price").val("");
    $("#name").focus();
}

function getProductsTable() {
    const lst = getProducts();
    let htmlRows = "";
    let count = 0;
    lst.forEach((item) => {
        const htmlRow = `
    <tr>
      <td>${++count}</td>
      <td>${item.name}</td>
      <td>${item.description}</td>
      <td>${item.price}</td>
      <td><button type="button" class="btn btn-light mx-2" onclick="plusCart('${item.id}')">+</button>
      ${item.quantity}<button type="button" class="btn btn-light mx-2" onclick="minusCart('${item.id}')">-</button></td>
      <td>${item.total} Ks</td>
    </tr>
        `;
        htmlRows += htmlRow;
    });
    $("#tbody").html(htmlRows);
}

function plusCart(id){
  let productLst = getProducts();
  console.log(productLst);
  let product = productLst.findIndex((x)=> x.id === id);
  if(product === -1){
    errorMessage("No Data Found.");
    return;
  }
  productLst[product].quantity += 1;
  productLst[product].total = productLst[product].price * productLst[product].quantity;
  console.log(total);
  const addStr = JSON.stringify(productLst);
  localStorage.setItem(tblProduct, addStr);
  getProductsTable(); 

}

function minusCart(id){
  let productLst = getProducts();
  console.log(productLst);
  let product = productLst.findIndex((x)=> x.id === id);
  if(product === -1){
    errorMessage("No Data Found.");
    return;
  }
  else if (productLst[product].quantity > 0) {
  productLst[product].quantity -= 1;
  productLst[product].total = productLst[product].price * productLst[product].quantity;
  console.log(total);
  const addStr = JSON.stringify(productLst);
  localStorage.setItem(tblProduct, addStr);
  getProductsTable(); 
  }
}

